using Newtonsoft.Json;
using NKAPIService.API.Channel.Models;
using NKAPIService.API.Converter;

namespace NKAPIService.API.Channel
{
    public sealed class RequestRegisterChannel : IRequset
    {
        public const string Resource = "/v2/va/register-channel";

        [JsonProperty("nodeId")]
        public string NodeId { get; set; }

        [JsonProperty("channelName")]
        public string ChannelName { get; set; }

        [JsonProperty("inputUri")]
        public string InputURI { get; set; }

        [JsonProperty("inputType")]
        [JsonConverter(typeof(StringToInputTypeConverter))]
        public InputType InputType { get; set; }

        [JsonProperty("siblings")]
        public string[] Siblings { get; set; }

        [JsonIgnore]
        public RequestType RequsetType => RequestType.RegisterChannel;

        public string GetResource() => Resource;
    }

    public sealed class ResponseRegisterChannel : ResponseBase
    {
        [JsonProperty("channelId")]
        public string ChannelId { get; set; }
        [JsonProperty("rtspUrl")]
        public string RtspURL { get; set; }
    }
}
