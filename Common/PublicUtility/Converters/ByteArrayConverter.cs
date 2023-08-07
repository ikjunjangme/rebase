using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PublicUtility.Converters
{
    public static class ByteArrayConverter
    {

        public static ((int x, int y, int w, int h) fullbox, (int x, int y, int w, int h) objectbox)
        ConvertThumbnailSize2(int maxWidth, int maxHeight, (int x, int y, int w, int h) Box)
        {
            //(double width, double height) ratio  = (0.58, 0.42); //(340:140 기준)
            (double XPos, double YPos)    center = (Box.x + (Box.w / 2), Box.y + (Box.h / 2));
            (int x, int y, int w, int h) scalingImage = new(0,0,340,140);

            double scaling;
            double full_capture_width;
            double full_capture_height;
            // width가 큰거
            if (Box.w > Box.h)
            {
                double div = maxWidth / Box.w * 0.4;
                if (div > 0.7f)
                    div = 1.2f;
                scaling = div * Box.w * 0.5f;
                full_capture_width = scaling;
                full_capture_height = scaling * 0.62f;
            }
            // height가 큰거
            else
            {
                double div = maxWidth / Box.w * 0.4;
                if (div > 0.7f)
                    div = 1.2f;
                scaling = Box.h * div * 0.5f;
                full_capture_width = scaling * 1.38f;
                full_capture_height = scaling;
            }

            //정상 사이즈
            scalingImage.x = (int)(center.XPos - full_capture_width);
            scalingImage.y = (int)(center.YPos - full_capture_height);
            scalingImage.w = (int)(full_capture_width * 2);
            scalingImage.h = (int)(full_capture_height * 2);
            //음수 값일때
            if(scalingImage.x < 0)
            {
                scalingImage.w += Math.Abs(scalingImage.x); //벗어난 만큼 추가
                scalingImage.x = 0;
            }
            if(scalingImage.y < 0)
            {
                scalingImage.h += Math.Abs(scalingImage.y); //벗어난 만큼 추가
                scalingImage.y = 0;
            }
            //양수 값일때
            if(scalingImage.x + scalingImage.w > maxWidth)
            {
                int cropLength = (scalingImage.x + scalingImage.w) - maxWidth;
                scalingImage.x -= cropLength;
                scalingImage.w -= cropLength;
            }
            if (scalingImage.y + scalingImage.h > maxHeight)
            {
                int cropLength = (scalingImage.y + scalingImage.h) - maxHeight;
                scalingImage.y -= cropLength;
                scalingImage.h -= cropLength;
            }

            scalingImage.x = ImageConverter.ConvertMinMaxValuable(scalingImage.x, 0, maxWidth);
            scalingImage.y = ImageConverter.ConvertMinMaxValuable(scalingImage.y, 0, maxHeight);
            scalingImage.w = ImageConverter.ConvertMinMaxValuable(scalingImage.w, 0, maxWidth);
            scalingImage.h = ImageConverter.ConvertMinMaxValuable(scalingImage.h, 0, maxHeight);
            //Box 만들자
            (int x, int y, int w, int h) scalingBox = (Box.x - scalingImage.x, Box.y - scalingImage.y, Box.w, Box.h);

            return (scalingImage, scalingBox);
        }

        //특정 사이즈 비로 박스 자르기
        public static ((int x, int y, int w, int h) fullbox, (int x, int y, int w, int h) objectbox)
            ConvertThumbnailSize(int maxWidth, int maxHeight, (int x, int y, int w, int h) originBox)
        {
            (double width, double height) capture = (340, 140);
            double div = capture.height / capture.width;
            (double width, double height) ratio = (Math.Round(1 - div, 2), Math.Round(div, 2));
            (int x, int y) centerPos = (originBox.x + (int)(originBox.w * 0.5), originBox.y + (int)(originBox.h * 0.5));
            (int row, int col) convert;
            //1. 세로 비가 더 클 경우 - 세로비 기준으로 가로비를 늘려줌
            if (originBox.w < originBox.h)
            {
                double convertRatio = originBox.h * 1.5;
                convert.row = (int)(convertRatio * ratio.width);
                convert.col = (int)(convertRatio * ratio.height);
            }
            //2. 가로 비가 더 클 경우 - 가로비 기준으로 세로비를 늘려줌
            else
            {
                double convertRatio = originBox.w * 1.5;
                convert.row = (int)(convertRatio * ratio.width);
                convert.col = (int)(convertRatio * ratio.height);
            }

            var fixFullBox = (X: centerPos.x - convert.row, 
                              Y: centerPos.y - convert.col,
                              W: convert.row * 2, H: convert.col * 2);
            //최소 사이즈
            if (fixFullBox.X < 0)
            {
                fixFullBox.W -= fixFullBox.X;
                fixFullBox.X = 0;
            }
            if (fixFullBox.Y < 0)
            {
                fixFullBox.H -= fixFullBox.Y;
                fixFullBox.Y = 0;
            }

            //최대 사이즈
            if (fixFullBox.X + fixFullBox.W > maxWidth)
            {
                int oversize = (fixFullBox.X + fixFullBox.W) - maxWidth;
                fixFullBox.X = fixFullBox.X - oversize;
                fixFullBox.W = fixFullBox.W - oversize;
            }
            if (fixFullBox.Y + fixFullBox.H > maxHeight)
            {
                int oversize = (fixFullBox.Y + fixFullBox.H) - maxHeight;
                fixFullBox.Y = fixFullBox.Y - oversize;
                fixFullBox.H = fixFullBox.H - oversize;
            }

            var fixObjectBox = (X: originBox.x - fixFullBox.X,
                                Y: originBox.y - fixFullBox.Y,
                                W: originBox.w, 
                                H: originBox.h);

            //fix full
            fixFullBox.X = ImageConverter.ConvertMinMaxValuable(fixFullBox.X, 0, maxWidth);
            fixFullBox.Y = ImageConverter.ConvertMinMaxValuable(fixFullBox.Y, 0, maxHeight);
            fixFullBox.W = ImageConverter.ConvertMinMaxValuable(fixFullBox.W, 0, maxWidth - fixFullBox.W);
            fixFullBox.H = ImageConverter.ConvertMinMaxValuable(fixFullBox.H, 0, maxHeight - fixFullBox.H);

            //fix Object
            fixObjectBox.X = ImageConverter.ConvertMinMaxValuable(fixObjectBox.X, 5, fixFullBox.W);
            fixObjectBox.Y = ImageConverter.ConvertMinMaxValuable(fixObjectBox.Y, 5, fixFullBox.H);
            fixObjectBox.W = ImageConverter.ConvertMinMaxValuable(fixObjectBox.W, 0, fixFullBox.W - 5);
            fixObjectBox.H = ImageConverter.ConvertMinMaxValuable(fixObjectBox.H, 0, fixFullBox.H - 5);

            return (fixFullBox, fixObjectBox);
        }

        //정규화 값을 해상도의 픽셀 단위 값으로 변경
        public static (int x, int y, int w, int h) ConvertPixelCoordinate(int width, int height, double nx, double ny, double nw, double nh)
        {
            int pX = (int)(width * nx);
            int pY = (int)(height * ny);
            int pWidth = (int)(width * nw);
            int pHeight = (int)(height * nh);

            if (pX < 0) pX = 0;
            if (pY < 0) pY = 0;
            if (pWidth < 0) pWidth = 0;
            if (pHeight < 0) pHeight = 0;
            if (pX > width) pX = width;
            if (pY > height) pY = height;
            if (pWidth > width) pWidth = width;
            if (pHeight > height) pHeight = height;

            return (pX, pY, pWidth, pHeight);
        }

        public static (byte[] buffer, int width, int height) GetCroppingNoramlize(this byte[] srcBytes, int srcWidth, int srcHeight, 
            (int x, int y, int w, int h) cropBox)
        {
            if (cropBox.w % 4 != 0)
            {
                cropBox.w -= cropBox.w % 4;
            }

            if (cropBox.h % 4 != 0)
            {
                cropBox.h -= cropBox.h % 4;
            }

            if (cropBox.x < 0) cropBox.x = 0;
            if (cropBox.y < 0) cropBox.y = 0;
            if (cropBox.w < 0) cropBox.w = 0;
            if (cropBox.h < 0) cropBox.h = 0;

            int blockSize = 3;
            int stride = cropBox.w * blockSize;
            int pixel_size = srcWidth * srcHeight * blockSize;
            byte[] outputPixels = new byte[stride * cropBox.h];


            //Parallel.For(0, cropBox.h - 1, index =>
            //{
            //    int sourceIndex = ((cropBox.y + index) * srcWidth + cropBox.x) * blockSize;
            //    var destinationIndex = index * stride;
            //    if (pixel_size > sourceIndex + stride)
            //        Array.Copy(srcBytes, sourceIndex, outputPixels, destinationIndex, stride);
            //});


            for (int line = 0; line < cropBox.h - 1; line++)
            {
                int sourceIndex = ((cropBox.y + line) * srcWidth + cropBox.x) * blockSize;
                var destinationIndex = line * stride;
                if (pixel_size > sourceIndex + stride)
                {
                    Array.Copy(srcBytes, sourceIndex, outputPixels, destinationIndex, stride);
                }
            }

            return (outputPixels, cropBox.w, cropBox.h);
        }
        public static byte[] GetCropping(this byte[] srcBytes, int srcWidth, int srcHeight, int cropX, int cropY, int cropWidth, int cropHeight)
        {
            if (cropX < 0) cropX = 0;
            if (cropY < 0) cropY = 0;
            if (cropWidth < 0) cropWidth = 0;
            if (cropHeight < 0) cropHeight = 0;

            int blockSize = 3;
            int stride = cropWidth * blockSize;
            int pixel_size = srcWidth * srcHeight * blockSize;
            byte[] outputPixels = new byte[stride * cropHeight];

            //Parallel.For(0, cropHeight - 1, index =>
            //{
            //    int sourceIndex = ((cropY + index) * srcWidth + cropX) * blockSize;
            //    var destinationIndex = index * stride;
            //    if (pixel_size > sourceIndex + stride)
            //        Array.Copy(srcBytes, sourceIndex, outputPixels, destinationIndex, stride);
            //});

            for (int line = 0; line < cropHeight - 1; line++)
            {
                int sourceIndex = ((cropY + line) * srcWidth + cropX) * blockSize;
                var destinationIndex = line * stride;
                if (pixel_size > sourceIndex + stride)
                {
                    Array.Copy(srcBytes, sourceIndex, outputPixels, destinationIndex, stride);
                }
            }

            return outputPixels;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public unsafe static System.IntPtr ToAddress(this byte[] array)
        {
            if (array == null) return System.IntPtr.Zero;

            fixed (byte* ptr = array)
            {
                return (System.IntPtr)(ptr - 2 * sizeof(void*));
            }
        }
    }
}
