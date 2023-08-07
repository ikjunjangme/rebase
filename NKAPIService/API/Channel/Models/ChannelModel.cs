using Newtonsoft.Json;

namespace NKAPIService.API.Channel.Models
{
    public class ChannelModel
    {
        [JsonProperty("channelId")]
        public string ChannelId { get; set; }
        [JsonProperty("nodeId")]
        public string NodeId { get; set; }

        [JsonProperty("inputUri")]
        public string InputUri { get; set; }

        [JsonProperty("channelName")]
        public string ChannelName { get; set; }

        [JsonProperty("inputDeviceType")]
        public InputType InputDeviceType { get; set; }
        [JsonProperty("siblings")]
        public string[] Siblings { get; set; }
        [JsonProperty("status")]
        public ChannelStatus Status { get; set; }
    }
}
