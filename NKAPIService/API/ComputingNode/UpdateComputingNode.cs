using Newtonsoft.Json;

namespace NKAPIService.API.ComputingNode
{
    public class RequestUpdateComputingNode : IRequset
    {
        public const string Resource = "/v2/va/udpate-computing-node";

        [JsonProperty("nodeId")]
        public string NodeId { get; set; }

        [JsonProperty("host")]
        public string Host { get; set; }

        [JsonProperty("httpPort")]
        public int HttpPort { get; set; }

        [JsonProperty("rpcPort")]
        public string RpcPort { get; set; }

        [JsonProperty("nodeName")]
        public string NodeName { get; set; }

        public RequestType RequsetType => RequestType.UpdateComputingNode;
        public string GetResource() => Resource;
    }

    public class ResponseUpdateComputingNode : ResponseBase
    {
        [JsonProperty("nodeId")]
        public string NodeId { get; set; }
    }
}
