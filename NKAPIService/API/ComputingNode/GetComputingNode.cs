using Newtonsoft.Json;
using NKAPIService.API.ComputingNode.Models;

namespace NKAPIService.API.ComputingNode
{
    public class RequestGetComputingNode : IRequset
    {
        public const string Resource = "/v2/va/get-computing-node";

        [JsonProperty("nodeId")]
        public string NodeId { get; set; }

        public RequestType RequsetType => RequestType.GetComputingNode;
        public string GetResource() => Resource;
    }

    public class ResponseGetComputingNode : ResponseBase
    {
        [JsonProperty("node")]
        public Node Node { get; set; }
    }
}
