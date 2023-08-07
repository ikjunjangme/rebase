using PredefineConstant.Enum.Analysis;
using PredefineConstant.Enum.Analysis.EventType;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace PredefineConstant
{
    public class EventInfo
    {
        public double AbnormalScore { get; set; }
        public bool IsEvent { get; set; }
        public ClassId ClassID { get; set; }
        public int ObjectID { get; set; } = -1;
        public Progress EventStatus { get; set; }
        public IntegrationEventType EventType { get; set; }
        public string RoiUId { get; set; }
        public double StayTime { get; set; }
        public byte[] ImageBuffer { get; set; }
        public RectangleF ImageRect { get; set; }
        public RectangleF InnerImageRect { get; set; }

        public EventDetail EventDetail { get; set; }
    }

    public class ObjectMeta
    {
        public string CameraUID { get; set; }
        public string CameraName { get; set; }
        public List<EventInfo> EventList { get; set; }
        public int FrameWidth { get; set; }
        public int FrameHeight { get; set; }
        public DateTime TimeStamp { get; set; }
    }

    public class EventDetail
    {
        public double EventScore { get; set; }
    }

    public class FaceEventDetail : EventDetail
    {
        public string FaceUID { get; set; }
    }
}
