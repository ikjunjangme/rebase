using Newtonsoft.Json;

namespace NKAPIService.API.Channel
{
    public sealed class RequestSnapshot : IRequset
    {
        public const string Resource = "/v2/va/snapshot";

        [JsonProperty("channelId")]
        public string ChannelId { get; set; }

        [JsonIgnore]
        public RequestType RequsetType => RequestType.Snapshot;
        public string GetResource() => Resource;
    }

    public sealed class ResponseSnapshot : ResponseBase
    {
        [JsonProperty("imageData")]
        public string ImageData { get; set; }
    }
}
