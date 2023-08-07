using NKAPI.API.Model;
using System.Collections.Generic;
using PublicUtility.Event.Enum;
using PublicUtility.Event.Class;

namespace NKAPI.API.Repuest
{
    public class RequestFace
    {
        public string nodeId { get; set; }
        public string faceId { get; set; }
        public List<string> faceImages { get; set; }
        public double matchingScore { get; set; }
    }
    public class RequestRoi
    {
        public string nodeId { get; set; }
        public string channelId { get; set; }
        public string roiId { get; set; }
        public List<string> roiIds { get; set; }
        public string roiName { get; set; }
        public EventType eventType { get; set; }
        public string description { get; set; }
        public int stayTime { get; set; }
        public int numberOf { get; set; }
        public RoiFeature feature { get; set; }
        public List<RoiDot> roiDots { get; set; }
        public EventFiltering eventFilter { get; set; }
    }
    public class RequestRoiLink
    {
        public string nodeId { get; set; }
        public string channelId { get; set; }
        public List<string> roiLink { get; set; }
        public string linkId { get; set; }
    }
    public class RequestVAControl
    {
        public string nodeid { get; set; }
        public List<string> channelIds { get; set; }
        //public List<string> parameter { get; set; }
        public VAOperations operation { get; set; }
        public int rpcPort { get; set; }
    }
    public class RequestChannel
    {
        public string nodeId { get; set; }
        public string channelId { get; set; }
        public string channelName { get; set; }
        public string inputUri { get; set; }
        public string inputType { get; set; }
        public int FrameWidth { get; set; }
        public int FrameHeight { get; set; }
        public List<string> siblings { get; set; }
        public Calibration calibration { get; set; }
        public List<ChannelInfo> channels { get; set; }
    }
    public class RequestCompute
    {
        public string nodeId { get; set; }
        public string nodeName { get; set; }
        public string host { get; set; }
        public int httpPort { get; set; }
        public string license { get; set; }
    }

}
