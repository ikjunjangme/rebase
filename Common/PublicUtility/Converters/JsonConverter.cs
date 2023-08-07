using Newtonsoft.Json;
#nullable enable
namespace PublicUtility.Converters
{
    public class JsonConverter
    {
        public static string Serialize(object? target)
        {
            return JsonConvert.SerializeObject(target, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            });
        }

        public static T? Deserialize<T>(string value)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(value, (JsonSerializerSettings?)null);
        }
    }
}
