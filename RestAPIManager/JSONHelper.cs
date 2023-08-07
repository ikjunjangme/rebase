using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace RestAPIManager
{
    public class JSONHelper
    {
        public static string GetJSONStringFromObject(object o)
        {
            var option = new JsonSerializerOptions();
            option.IgnoreNullValues = true;
            return JsonSerializer.Serialize(o,option);
        }

        public static byte[] GetUTF8ByteArrayFromeObject(object o)
        {
            var option = new JsonSerializerOptions();
            option.IgnoreNullValues = true;
            return JsonSerializer.SerializeToUtf8Bytes(o, option);
        }

        public static T GetObjectFromJSONString<T>(string json)
        {
            //ytgu 테스트를 위해 개행문자 제거...
            //var tmp = json.Replace("\n", "").Replace("\r", "").Trim();
            T obj = default(T);
            try
            {
                obj = JsonSerializer.Deserialize<T>(json);
            }
            catch (Exception ex)
            {

            }
            return obj;
        }
    }
}
