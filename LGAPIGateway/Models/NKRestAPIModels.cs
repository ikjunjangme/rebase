using NKAPI.API.Model;
using NKAPI.API.Response;
using PublicUtility.Event.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGAPIGateway.Models
{
    public class ResponseCode
    {
        public int code { get; set; }
        public string message { get; set; }
    }
    public class ResponseChannelInfo
    {
        public string channelId { get; set; }
        public string rtspUrl { get; set; }
    }
    public class ResponseChannelMsg
    {
        public string channelId { get; set; }
        public string rtspUrl { get; set; }
        public List<ResponseChannelInfo> channels { get; set; }
        public int code { get; set; }
        public string message { get; set; }
    }
    public class ResponseChannelListMsg
    {
        public List<ChannelInfo> channels { get; set; }
        public int code { get; set; }
        public string message { get; set; }
    }
    public class ResponseChannelInfoMsg
    {
        public string nodeId { get; set; }
        public string channelId { get; set; }
        public string inputUri { get; set; }
        public string channelName { get; set; }
        public InputType inputType { get; set; }
        public List<string> siblings { get; set; }
        public ChannelStatus status { get; set; }
        public int FrameWidth { get; set; }
        public int FrameHeight { get; set; }
        public int code { get; set; }
        public string message { get; set; }
    }
    public class ResponseRoiMsg
    {
        public string roiId { get; set; }
        public int code { get; set; }
        public string message { get; set; }
    }
    public class ResponseRoiListMsg
    {
        public List<ResponseRoiInfo> rois { get; set; }
        public int rpcPort { get; set; }
        public int code { get; set; }
        public string message { get; set; }
    }
    public class ResponseRoiInfoMsg
    {
        public string roiId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public double stayTime { get; set; }
        public int numberOf { get; set; }
        public EventType eventType { get; set; }
        public RoiFeature feature { get; set; }
        public List<RoiDot> roiDots { get; set; }
        public int code { get; set; }
        public string message { get; set; }
    }

    public class ResponseLinkMsg
    {
        public string linkId { get; set; }
        public int code { get; set; }
        public string message { get; set; }
    }

    public class ResponseVacStartMsg
    {
        public string sourceIp { get; set; }
        public int sourcePort { get; set; }
        public int code { get; set; }
        public string message { get; set; }
    }
    public class ReponseVacMsg
    {
        public int code { get; set; }
        public string message { get; set; }
    }

    public class ResponseComputingNodeMsg
    {
        public string nodeId { get; set; }
        public string nodeName { get; set; }
        public string httpHost { get; set; }
        public int httpPort { get; set; }
        public string rpcHost { get; set; }
        public string license { get; set; }
        public string productCode { get; set; }
        public string releaseDate { get; set; }
        public int code { get; set; }
        public string message { get; set; }
    }

    public class ResponseSystemInfoMsg
    {
        public string nodeId { get; set; }
        public SystemVersionInfo version { get; set; }
        public int countVAChannels { get; set; }
        public List<string> warningChannels { get; set; }
        public SystemPerformanceInfo performance { get; set; }
        public int code { get; set; }
        public string message { get; set; }
    }

    public class ResponseFaceInfo
    {
        public string faceId { get; set; }
        public int code { get; set; }
        public string message { get; set; }
    }
}
