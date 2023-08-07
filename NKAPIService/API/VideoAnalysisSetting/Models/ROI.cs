using Newtonsoft.Json;
using System.Collections.Generic;

namespace NKAPIService.API.VideoAnalysisSetting.Models
{
    public class ROI
    {
        [JsonProperty("roiName")]
        public string ROIName { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("stayTime")]
        public int StayTime { get; set; }
        [JsonProperty("numberOf")]
        public int NumberOf { get; set; }
        [JsonProperty("feature")]
        public ROIFeature ROIFeature { get; set; }
        [JsonProperty("roiDots")]
        public List<ROIDot> ROIDots { get; set; }
        [JsonProperty("EventFilter")]
        public EventFilter EventFilter { get; set; }
    }
}
