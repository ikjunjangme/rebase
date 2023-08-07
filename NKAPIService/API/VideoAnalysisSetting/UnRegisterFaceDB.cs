using Newtonsoft.Json;

namespace NKAPIService.API.VideoAnalysisSetting
{
    public class RequestUnRegisterFaceDB : IRequset
    {
        public const string Resource = "/v2/va/remove-facedb";

        [JsonProperty("nodeId")]
        public string NodeId { get; set; }
        [JsonProperty("faceId")]
        public string FaceId { get; set; }

        public RequestType RequsetType => RequestType.UnRegisterFaceDB;
        public string GetResource() => Resource;
    }

    public class ResponseUnRegisterFaceDB : ResponseBase
    {
        
    }
}
