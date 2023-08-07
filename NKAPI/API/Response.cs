using NKAPI.API.Model;
using PublicUtility.Event.Enum;
using System.Collections.Generic;

namespace NKAPI.API.Response
{
    public class Node
    {
        public string nodeId { get; set; }
    }

    public class ResponseOnlyNode
    {
        public Node node { get; set; }
        public string message { get; set; }
        public int code { get; set; }
    }
    public class ResponseVacMsg
    {
        public string sourceIp { get; set; }
        public int sourcePort { get; set; }
        public int code { get; set; }
        public string message { get; set; }
    }
    public class ResponseRoiInfos
    {
        public List<ResponseRoiInfo> rois { get; set; }
    }
    public class ResponseRoiList
    {
        public List<ResponseRoiInfo> rois { get; set; }
        public int rpcPort { get; set; }
    }
    public class ResponseRoiInfo
    {
        public string roiId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public double stayTime { get; set; }
        public int numberOf { get; set; }
        public int eventType { get; set; }
        public RoiFeature feature { get; set; }
        public List<RoiDot> roiDots { get; set; }
        public int code { get; set; }
        public string message { get; set; }
    }
    public class ResponseChannel
    {
        public string nodeId { get; set; }
        public string channelId { get; set; }
        public string channelName { get; set; }
        public string imageData { get; set; }
        public ChannelInfo channel { get; set; }
        public List<ChannelInfo> channels { get; set; }
        public int code { get; set; }
    }
    public class ResponseCompute
    {
        public string nodeId { get; set; }
        public string host { get; set; }
        public int httpPort { get; set; }
        public string rpcHost { get; set; }
        public int rpcPort { get; set; }
        public string nodeName { get; set; }
        public string productCode { get; set; }
        public string productVersion { get; set; }
        public string description { get; set; }
        public string licenseValidity { get; set; }
        public string licenseExpired { get; set; }
        public Funtions[] functions { get; set; }
    }
    public class ResponseResource
    {
        public string nodeId { get; set; }
        public SystemVersionInfo version { get; set; }
        public int channelsInVaRunning { get; set; }
        public List<SystemWarnings> systemWarnings { get; set; }
        public List<ChannelInfo> channelsInWarning { get; set; }
        public SystemPerformanceInfo performance { get; set; }
        public int code { get; set; }
        public string message { get; set; }
    }
    public class ResponseComputingNodeListMsg
    {
        public List<ResponseComputingNode> nodes { get; set; }
        public int code { get; set; }
        public string message { get; set; }
    }
    public class ResponseComputingNode
    {
        public string nodeId { get; set; }
        public string nodeName { get; set; }
        public string httpHost { get; set; }
        public int httpPort { get; set; }
        public string rpcHost { get; set; }
        public int rpcPort { get; set; }
        public string license { get; set; }
        public string productCode { get; set; }
        public string releaseDate { get; set; }
        public string message { get; set; }
    }
}
