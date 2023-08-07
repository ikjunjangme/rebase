using Newtonsoft.Json;
using NKAPIService.API.Channel.Models;

namespace NKAPIService.API.Channel
{
    public sealed class RequestListChannels : IRequset
    {
        public const string Resource = "/v2/va/list-channels";

        [JsonIgnore]
        public RequestType RequsetType => RequestType.ListChannel;

        public string GetResource() => Resource;
    }

    public sealed class ResponseListChannels : ResponseBase
    {
        public ChannelModel[] ChannelModels { get; set; }
    }
}
