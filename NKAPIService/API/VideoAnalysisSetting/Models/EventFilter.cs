using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace NKAPIService.API.VideoAnalysisSetting.Models
{
    public class EventFilter
    {
        [JsonProperty("minDetectSize")]
        public int MinDetectSize { get; set; }
        [JsonProperty("maxDetectSize")]
        public int MaxDetectSize { get; set; }

        [JsonProperty("objectsTarget")]
        public List<ObjectType> ObjectsTarget { get; set; }
    }
}
