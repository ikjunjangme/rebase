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
    public static class ImageConverter
    {
        public static void Savebase64JpegImage(string base64Image, string filePath)
        {
            var bytess = Convert.FromBase64String(base64Image);
            using (var imageFile = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
            {
                imageFile.Write(bytess, 0, bytess.Length);
                imageFile.Flush();
            }
        }

        public static byte[] ToByteFromBase64(this string base64Image)
        {
            return Convert.FromBase64String(base64Image);
        }

        public static Bitmap ResizeImage(this Bitmap image, Rectangle to)
        {
            if (image.Width == to.Width && image.Height == to.Height) return image;

            var destImage = new Bitmap(to.Width, to.Height, image.PixelFormat);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(System.Drawing.Drawing2D.WrapMode.TileFlipXY);
                    graphics.DrawImage(image, to, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public static Bitmap ToImage(this byte[] data, int x, int y, int w, int h, PixelFormat pixelformat = PixelFormat.Format24bppRgb)
        {
            Rectangle from = new Rectangle(x,y,w,h);
            byte[] imageBytes = data;

            // 기본 rgb 형식으로 만들고

            int pix = 4;
            // 원본 이미지가 4의 배수가 아니면 맞춰줌 아니면 깨짐..
            int addWidth = from.Width % pix;
            int addHeight = from.Height % pix;
            if ((addWidth != 0) || (addHeight != 0))
            {
                int beforeWidth = from.Width;
                int beforeHeight = from.Height;
                int bit = pixelformat == PixelFormat.Format24bppRgb ? 3 : 4;

                from.Width += addWidth;
                from.Height += addHeight;
                imageBytes = new byte[from.Width * from.Height * bit];

                int index = 0;

                Parallel.For(0, beforeHeight, i =>
                {
                    Array.Copy(data, index, imageBytes, from.Width * bit * i, beforeWidth * bit);
                    index += (beforeWidth * bit);
                });

                //for (int i = 0; i < beforeHeight; i++)
                //{
                //    Array.Copy(data, index, imageBytes, from.Width * bit * i, beforeWidth * bit);
                //    index += (beforeWidth * bit);
                //}
            }
            Bitmap bitmap = new Bitmap(from.Width, from.Height, pixelformat);
            Rectangle rect = new Rectangle(0, 0, from.Width, from.Height);
            BitmapData bitmapData = bitmap.LockBits(rect, ImageLockMode.WriteOnly, bitmap.PixelFormat);
            Marshal.Copy(imageBytes, 0, bitmapData.Scan0, imageBytes.Length);
            bitmap.UnlockBits(bitmapData);

            if (pixelformat != PixelFormat.Format24bppRgb)
            {
                // parameter가 다를경우엔 아래에서 새로 만들어서 반환
                bitmap = ConvertTo24bpp(bitmap);
            }

            return bitmap;
        }

        public static Bitmap ConvertTo24bpp(Image img)
        {
            var bmp = new Bitmap(img.Width, img.Height, PixelFormat.Format24bppRgb);
            using (var gr = Graphics.FromImage(bmp))
                gr.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height));
            return bmp;
        }

        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                    return codec;
            }
            return null;
        }

        public static double ConvertMinMaxValuable(double value, double min, double max)
        {
            double convertValue = value;
            if (value < min) 
                convertValue = min;
            else if (value > max) 
                convertValue = max;

            return Math.Round(convertValue,2);
        }

        public static int ConvertMinMaxValuable(int value, int min, int max)
        {
            int convertValue = value;
            if (value < min) 
                convertValue = min;
            else if (value > max) 
                convertValue = max;

            return convertValue;
        }

        public static byte[] EncodingJpeg(this byte[] bytes, int orgWidth, int orgHeight, long value = 50L)
        {
            byte[] output = null;
            using (var bmp = bytes.ToImage(0, 0, orgWidth, orgHeight))
            {
                using (var mem = new MemoryStream())
                {
                    EncoderParameters myEncoderParameters = new EncoderParameters(1);
                    myEncoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, value);
                    ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
                    //bmp.Save($@"sampleSaveImage.jpg", jpgEncoder, myEncoderParameters);
                    bmp.Save(mem, jpgEncoder, myEncoderParameters);
                    output = mem.ToArray();
                }
            }

            return output;
        }

        public static Bitmap ToBitmapFromJpegImage(this byte[] bytes)
        {
            Bitmap bmpReturn = null;
            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                memoryStream.Position = 0;
                bmpReturn = (Bitmap)Image.FromStream(memoryStream);
                memoryStream.Close();
            }

            return bmpReturn;
        }

        public static byte[] ToBytesFromJpegImage(this byte[] bytes)
        {
            byte[] bmpReturn = null;
            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                //img.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                //memoryStream.Position = 0;
                bmpReturn = memoryStream.ToArray();
                //bmpReturn = (byte)Image.FromStream(memoryStream);
                memoryStream.Close();
            }

            return bmpReturn;
        }

        public static int MultipleOf(int value, int multi = 4)
        {
            if (value % multi != 0)
                value -= value % multi;

            return value;
        }

        public static (int X, int Y, int W, int H) InflateBbox((int W, int H) origin, 
                                                                float alpha, (int X, int Y, int W, int H) srcBbox)
        {
            float margin = Math.Max(srcBbox.W, srcBbox.H) * alpha;
            float l = srcBbox.X - margin;
            if (l < 0.0f)
                l = 0.0f;

            float t = srcBbox.Y - margin;
            if (t < 0.0f)
                t = 0.0f;

            float r = srcBbox.X + srcBbox.W + (int)margin;
            if (r > origin.W)
                r = r - (r - origin.W);

            float b = srcBbox.Y + srcBbox.H + (int)margin;
            if (b > origin.H)
                b = b - (b - origin.H);

            var InflateBox = (X:(int)l, Y:(int)t, W:(int)(r - l), H:(int)(b - t));

            if (InflateBox.W <= 0 || InflateBox.H <= 0)
                return InflateBox;

            if (InflateBox.X < 0)
                InflateBox.X = 0;
            if (InflateBox.Y < 0)
                InflateBox.Y = 0;
            if (InflateBox.W > origin.W)
                InflateBox.W = InflateBox.W - (InflateBox.X + InflateBox.W - origin.W);
            if (InflateBox.H > origin.H)
                InflateBox.H = InflateBox.H - (InflateBox.Y + InflateBox.H - origin.H);

            if (InflateBox.W % 4 != 0)
                InflateBox.W = InflateBox.W / 4 * 4;

            return InflateBox;
        }

        public static ImageInfo GetBase64JpgObject(byte[] buffer, int width, int height, (double X, double Y, double W, double H) segmentaion)
        {
            var pixelBox = ByteArrayConverter.ConvertPixelCoordinate(width, height, segmentaion.X, segmentaion.Y, segmentaion.W, segmentaion.H);
            var convertBox = ByteArrayConverter.ConvertThumbnailSize2(width, height, pixelBox);
            var cropImage = ByteArrayConverter.GetCroppingNoramlize(buffer, width, height, convertBox.fullbox);
            var ecoding = EncodingJpeg(cropImage.buffer, cropImage.width, cropImage.height, 90L);
            var nrmBox = (X: (double)convertBox.objectbox.x / convertBox.fullbox.w,
                                Y: (double)convertBox.objectbox.y / convertBox.fullbox.h,
                                W: (double)convertBox.objectbox.w / convertBox.fullbox.w,
                                H: (double)convertBox.objectbox.h / convertBox.fullbox.h);

            nrmBox.X = ConvertMinMaxValuable(nrmBox.X, 0, 1);
            nrmBox.Y = ConvertMinMaxValuable(nrmBox.Y, 0, 1);
            nrmBox.W = ConvertMinMaxValuable(nrmBox.W, 0, 1);
            nrmBox.H = ConvertMinMaxValuable(nrmBox.H, 0, 1);

            return new ImageInfo
            {
                base64jpg = Convert.ToBase64String(ecoding),
                width  = convertBox.fullbox.w,
                height = convertBox.fullbox.h,
                cropX = nrmBox.X,
                cropY = nrmBox.Y, 
                cropW = nrmBox.W,
                cropH = nrmBox.H
            };
        }

        //test
        //EncoderParameters encoderParameters = new EncoderParameters(1);
        //encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
        //bitmap.Save("save.jpg", ImageFormat.Jpeg);
        //using (var ms = new MemoryStream(JpgBytes))
        //{
        //    using (var fs = new FileStream("save_test.jpg", FileMode.Create))
        //    {
        //        ms.WriteTo(fs);
        //    }
        //}
    }
    public class ImageInfo
    {
        public string base64jpg { get; set; } = "";
        public int width { get; set; } = 0;
        public int height { get; set; } = 0;
        public double cropX { get; set; } = 0.0f;
        public double cropY { get; set; } = 0.0f;
        public double cropW { get; set; } = 0.0f;
        public double cropH { get; set; } = 0.0f;
    }
}
