using PublicUtility.Event.Enum;

namespace PublicUtility
{
    public class DetectedAnnotationInfo
    {
        public int EtcObjectTrackingId { get; set; }
        public ObjectType Label { get; set; }
        public double Confidence { get; set; }
        public (int X, int Y, int W, int H) Box { get; set; }
        //public List<(float X, float Y)> landmarks { get; set; }
        //public List<float> keypoints { get; set; }
        public string base64Image { get; set; }
        public double trackingTime { get; set; }
        public bool UnMasked { get; set; }
        public double MaskCondition { get; set; }
        public bool Matched { get; set; }
        public double MatchCondition { get; set; }
        public string MatchedUid { get; set; }
        public int HelmetScore { get; set; }
    }

    public class DetectedVirtualEventInfo
    {
        public int trackId { get; set; }
        public ObjectType objType { get; set; }
        public (double X, double Y, double W, double H) normalizeBox { get; set; }
        public string base64Image { get; set; }
        public string roiUid { get; set; }
        public int eventType { get; set; }
        public string eventMessage { get; set; }
        public double eventScore { get; set; }
        public int eventState { get; set; }
    }
    public enum DetailType
    {
        MASK,
        MATCH_ID,
        MATCH,
        STAYTIME
    }
}
