using Newtonsoft.Json;
using System.Collections.Generic;

namespace NKAPIService.API.VideoAnalysisSetting.Models
{
    public class ROILine
    {
        [JsonProperty("disable")]
        public bool Disable { get; set; }
        [JsonProperty("direction")]
        public string Direction { get; set; }
        [JsonProperty("target")]
        public List<ObjectType> ObjectTarget { get; set; }
    }
}
