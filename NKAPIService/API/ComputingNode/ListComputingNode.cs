using Newtonsoft.Json;
using NKAPIService.API.ComputingNode.Models;
using System.Collections.Generic;

namespace NKAPIService.API.ComputingNode
{
    public class RequestListComputingNode : IRequset
    {
        public const string Resource = "/v2/va/list-computing-node";

        public RequestType RequsetType => RequestType.ListComputingNode;

        public string GetResource() => Resource;
        
    }

    public class ResponseListComputingNode : ResponseBase
    {
        [JsonProperty("nodes")]
        public List<Node> NodeInfos { get; set; }
    }
}
