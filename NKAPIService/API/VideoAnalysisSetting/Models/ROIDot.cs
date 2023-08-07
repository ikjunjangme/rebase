using Newtonsoft.Json;

namespace NKAPIService.API.VideoAnalysisSetting.Models
{
    public class ROIDot
    {
        [JsonProperty("x")]
        public double X { get; set; }
        [JsonProperty("y")]
        public double Y { get; set; }
        [JsonProperty("lineUntilNextkDot")]
        public ROILine LineUntilNextDot { get; set; }
    }
}
