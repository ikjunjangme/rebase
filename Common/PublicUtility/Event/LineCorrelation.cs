using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicUtility.Event
{
    public static class LineCorrelation
    {        
        //직선 교점 구하기
        public static bool FindIntersection((double X, double Y) objpt1, (double X, double Y) objpt2, (double X, double Y) linept1, (double X, double Y) linept2)
        {
            if (objpt1.X < 0 || objpt1.Y < 0) return false;

            double d = (objpt1.X - objpt2.X) * (linept1.Y - linept2.Y) - (objpt1.Y - objpt2.Y) * (linept1.X - linept2.X);

            if (d == 0)
                return false;

            double t = (linept2.X - linept1.X) * (objpt1.Y - linept1.Y) - (linept2.Y - linept1.Y) * (objpt1.X - linept1.X);
            double s = (objpt2.X - objpt1.X) * (objpt1.Y - linept1.Y) - (objpt2.Y - objpt1.Y) * (objpt1.X - linept1.X);

            if (t == 0 && s == 0)
                return false;

            var tt = t / d;
            var ss = s / d;

            if (tt < 0.0 || tt > 1.0 || ss < 0.0 || ss > 1.0)
                return false;

            return true;
        }

        //직선 여분으로 엔터 구하기 위해서
        public static bool ParrallelLine((double X, double Y) objpt1, System.Drawing.Point pt1, System.Drawing.Point pt2, float ratio = 5)
        {
            double px = pt1.Y - pt2.Y;
            double py = pt2.X - pt1.X;
            double endLen = 15 * ratio; //distance between two parallel lines
            double len = endLen / Math.Sqrt(px * px + py * py);
            px *= len;
            py *= len;

            return Annotaion.GetCheckObjectInROI((objpt1.X, objpt1.Y), new List<System.Drawing.Point>
            {
                pt1,
                pt2,
                new System.Drawing.Point((int)(pt2.X + px), (int)(pt2.Y + py)),
                new System.Drawing.Point((int)(pt1.X + px), (int)(pt1.Y + py))
            });
        }
    }
}
