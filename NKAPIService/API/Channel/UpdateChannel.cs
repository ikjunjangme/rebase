using Newtonsoft.Json;

namespace NKAPIService.API.Channel
{
    public sealed class RequestUpdateChannel : IRequset
    {
        public const string Resource = "/v2/va/update-channel";

        [JsonProperty("channelId")]
        public string ChannelId { get; set; }

        [JsonProperty("nodeId")]
        public string NodeId { get; set; }

        [JsonProperty("inputUri")]
        public string InputUri { get; set; }

        [JsonProperty("inputType")]
        public string InputType { get; set; }

        [JsonProperty("siblings")]
        public object[] Siblings { get; set; }

        [JsonIgnore]
        public RequestType RequsetType => RequestType.UpdateChannel;

        public string GetResource() => Resource;
    }
}
