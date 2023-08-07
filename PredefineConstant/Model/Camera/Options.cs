using PredefineConstant.Enum.Camera;
using System.Drawing;

namespace PredefineConstant.Model.Camera
{
    public class Options
    {
        public string Codec { get; set; }
        public string Stream { get; set; }
        public bool AutoStart { get; set; }
        public bool IsTcp { get; set; }
        public string PtzConnectionMethod { get; set; }
        public string PtzCamType { get; set; }
        public string Calibration { get; set; } = "";
        public CameraOSDFlag OsdFlag { get; set; }
        public Size FrameSize { get; set; }
    }
}
