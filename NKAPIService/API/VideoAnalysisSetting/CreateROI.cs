using Newtonsoft.Json;
using NKAPIService.API.VideoAnalysisSetting.Models;
using System.Collections.Generic;

namespace NKAPIService.API.VideoAnalysisSetting
{
    public sealed class RequestCreateROI : IRequset
    {
        public const string Resource = "/v2/va/create-roi";

        [JsonProperty("nodeId")]
        public string NodeId { get; set; }
        [JsonProperty("channelId")]
        public string ChannelID { get; set; }
        [JsonProperty("eventType")]
        public EventType EventType { get; set; }
        [JsonProperty("roiName")]
        public string ROIName { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("stayTime")]
        public int StayTime { get; set; }
        [JsonProperty("numberOf")]
        public int NumberOf { get; set; }
        [JsonProperty("feature")]
        public ROIFeature ROIFeature { get; set; }
        [JsonProperty("roiDots")]
        public List<ROIDot> ROIDots { get; set; }
        [JsonProperty("EventFilter")]
        public EventFilter EventFilter { get; set; }

        public RequestType RequsetType => RequestType.CreateROI;
        public string GetResource() => Resource;
    }

    public class ResponseCreateROI : ResponseBase
    {
        [JsonProperty("roiId")]
        public string ROIID { get; set; }
    }
}
