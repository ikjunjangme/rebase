using Newtonsoft.Json;
using System.Collections.Generic;

namespace NKAPIService.API.VideoAnalysisSetting
{
    public class RequestRegisterFaceDB : IRequset
    {
        public const string Resource = "/v2/va/register-facedb";

        [JsonProperty("nodeId")]
        public string NodeId { get; set; }

        [JsonProperty("matchingScore")]
        public float MatchingScore { get; set; }

        [JsonProperty("faceImages")]
        public List<string> FaceImages { get; set; }

        [JsonIgnore]
        public RequestType RequsetType => RequestType.RegisterFaceDB;
        public string GetResource() => Resource;
    }

    public class ResponseRegisterFaceDB : ResponseBase
    {
        [JsonProperty("faceId")]
        public string FaceId { get; set; }
    }
}
