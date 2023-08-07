using Newtonsoft.Json;
using NKAPIService.API.VideoAnalysisSetting.Models;
using System.Collections.Generic;

namespace NKAPIService.API.VideoAnalysisSetting
{
    public class RequestListROI : IRequset
    {
        public const string Resource = "/v2/va/list-roi";

        [JsonProperty("channelId")]
        public string ChannelID { get; set; }

        public RequestType RequsetType => RequestType.ListROI;

        public string GetResource() => Resource;
    }

    public class ResponseListROI : ResponseBase
    {
        [JsonProperty("roiList")]
        public List<ROI> Rois { get; set; }
    }
}
