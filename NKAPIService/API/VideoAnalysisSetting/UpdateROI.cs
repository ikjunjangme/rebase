using Newtonsoft.Json;
using NKAPIService.API.VideoAnalysisSetting.Models;
using System.Collections.Generic;

namespace NKAPIService.API.VideoAnalysisSetting
{
    public sealed class RequestUpdateROI : IRequset
    {
        public const string Resource = "/v2/va/update-roi";

        [JsonProperty("channelId")]
        public string ChannelID { get; set; }
        [JsonProperty("roiId")]
        public string ROIId { get; set; }

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

        public RequestType RequsetType => RequestType.CreateROI;
        public string GetResource() => Resource;
    }
}
