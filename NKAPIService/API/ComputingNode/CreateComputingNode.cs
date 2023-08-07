using Newtonsoft.Json;

namespace NKAPIService.API.ComputingNode
{
    public class RequestCreateComputingNode : IRequset
    {
        public const string Resource = "/v2/va/create-computing-node";

        [JsonProperty("host")]
        public string Host { get; set; }

        [JsonProperty("httpPort")]
        public int HttpPort { get; set; }

        [JsonProperty("rpcPort")]
        public string RpcPort { get; set; }

        [JsonProperty("nodeName")]
        public string NodeName { get; set; }

        public RequestType RequsetType => RequestType.CreateComputingNode;
        public string GetResource() => Resource;
    }

    public class ResponseCreateComputingNode : ResponseBase
    {
        [JsonProperty("nodeId")]
        public string NodeId { get; set; }
    }
}
