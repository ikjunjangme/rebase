using Newtonsoft.Json;
using NKAPIService.API.VideoAnalysisSetting.Models;

namespace NKAPIService.API.VideoAnalysisSetting
{
    public class RequsetGetROI : IRequset
    {
        public const string Resource = "/v2/va/get-roi";
        [JsonProperty("roiId")]
        public string ROIID { get; set; }

        public RequestType RequsetType => RequestType.GetROI;

        public string GetResource() => Resource;
    }

    public class ResponseGetROI : ResponseBase
    {
        [JsonProperty("roi")]
        public ROI Roi { get; set; }
    }
}
