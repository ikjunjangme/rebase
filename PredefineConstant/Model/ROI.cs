using PredefineConstant.Enum.Analysis;
using System.Collections.Generic;
using System.Drawing;

namespace PredefineConstant.Model
{
    public class ROI
    {
        public DrawingType DrawingType { get; set; }
        public List<PointF> Points { get; set; }
        public Size DPI { get; set; }

        public ROI Clone()
        {
            return new ROI() { DPI = new Size(DPI.Width, DPI.Height), Points = new List<PointF>(Points.ToArray()) };
        }
    }
}
