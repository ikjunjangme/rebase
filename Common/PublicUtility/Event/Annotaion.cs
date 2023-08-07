using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace PublicUtility
{
    public static class Annotaion
    {
        //다각형 내부 점 판단 계산1
        public static bool GetCheckObjectInROI((double X, double Y) InputBoxCenterPos, List<(double X, double Y)> CheckRoi)
        {
            int cross = 0;
            int areaCount = CheckRoi.Count();
            for (int i = 0; i < areaCount; i++)
            {
                int j = (i + 1) % areaCount;
                if ((CheckRoi[i].Y > InputBoxCenterPos.Y) != (CheckRoi[j].Y > InputBoxCenterPos.Y))
                {
                    double atX = (CheckRoi[j].X - CheckRoi[i].X) * (InputBoxCenterPos.Y - CheckRoi[i].Y) / (CheckRoi[j].Y - CheckRoi[i].Y) + CheckRoi[i].X;
                    if (InputBoxCenterPos.X < atX)
                        cross++;
                }
            }
            return cross % 2 > 0;
        }
        public static bool GetCheckObjectInROI((double X, double Y) InputBoxCenterPos, List<Point> CheckRoi)
        {
            int cross = 0;
            int areaCount = CheckRoi.Count();
            for (int i = 0; i < areaCount; i++)
            {
                int j = (i + 1) % areaCount;
                if ((CheckRoi[i].Y > InputBoxCenterPos.Y) != (CheckRoi[j].Y > InputBoxCenterPos.Y))
                {
                    double atX = (CheckRoi[j].X - CheckRoi[i].X) * (InputBoxCenterPos.Y - CheckRoi[i].Y) / (CheckRoi[j].Y - CheckRoi[i].Y) + CheckRoi[i].X;
                    if (InputBoxCenterPos.X < atX)
                        cross++;
                }
            }

            return cross % 2 > 0;
        }

        //다각형 내부 점 판단 계산2
        public static bool IsPolygonInTarget(List<Point> Roi, double TargetX, double TargetY)
        {
            bool result = false; 
            int j = Roi.Count - 1; 
            for (int i = 0; i < Roi.Count; i++) 
            { 
                if (Roi[i].Y < TargetY && Roi[j].Y >= TargetY || Roi[j].Y < TargetY && Roi[i].Y >= TargetY)
                { 
                    double value = Roi[i].X + (TargetY - Roi[i].Y) / (Roi[j].Y - Roi[i].Y) * (Roi[j].X - Roi[i].X); 
                    if (value < TargetX) 
                    { 
                        result = !result;
                    } 
                } 
                j = i; 
            } 
            return result; 
        }

        public static bool UnsaftyBox((int Width, int Height) frame, (int X, int Y, int Width, int Height) box)
        {
            if (box.X < 0 || box.Y < 0) return true;
            if (frame.Width < 0 || frame.Height < 0) return true;
            if (box.X > frame.Width || box.Y > frame.Height) return true;
            if (box.X + box.Width > frame.Width || box.Height > frame.Height) return true;
            if (box.Width > frame.Width * 0.8 || box.Height > frame.Height * 0.8) return true;

            return false;
        }
        public static bool UnsaftyBox((int Width, int Height) frame, (float X, float Y, float Width, float Height) box)
        {
            //좌표 범위를 벗어날 경우
            if (box.X < 0 || box.Y < 0) return true;
            if (frame.Width < 0 || frame.Height < 0) return true;
            if (box.X > frame.Width || box.Y > frame.Height) return true;
            if (box.X + box.Width > frame.Width || box.Height > frame.Height) return true;
            if (box.Width > frame.Width * 0.8 || box.Height > frame.Height * 0.8) return true;
            //화면의 70% 차지할 경우
            //if (box.Width * box.Height > frame.Width * frame.Height * 0.7f) return true;

            return false;
        }
        public static (int X, int Y, int H, int W) BoundingBox(int x, int y, int w, int h, int width, int height)
        {
            int X = x;
            int Y = y;
            int W = w;
            int H = h;

            if (width < X + W)
                W -= X + W - width;
            if (height < Y + H)
                H -= Y + H - height;

            return (X, Y, W, H);
        }
        public static (int X, int Y, int H, int W) BoundingBox(float x, float y, float w, float h, int width, int height)
        {
            int X = (int)x;
            int Y = (int)y;
            int W = (int)w;
            int H = (int)h;

            //사이즈가 벗어날 경우 보정
            if(width < X + W)
                W -= X + W - width;
            if (height < Y + H)
                H -= Y + H - height;

            return (X,Y,W,H);
        }
    }
    public enum DETECTED_MODEL
    {
        NORMAL,
        FACE,
        FIRE,
        FIGHT,
        HEAD,
    }
}
