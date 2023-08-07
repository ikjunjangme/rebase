using NKAPI.API.Repuest;
using NKAPI.API.Response;
using NKAPI.Proxy.RestApi;
using PublicUtility.HTTP;
using JsonConverter = PublicUtility.Converters.JsonConverter;

namespace NKAPI.API.v2
{
    public class ComputingNode
    {
        public static string Create(string path, string payload)
        {
            var req = JsonConverter.Deserialize<RequestCompute>(payload);
            var url = $"http://{req.host}:{req.httpPort}";
            var res = Client.RequestJsonbyPostAsync(url, path, payload); 

            return res?.Result.error == (int)HTTPStatusCode.Ok ? res.Result.response : null;
        }
        public static string GetNode(string url, string path, string payload)
        {
            if (url != null)
            {
                var res = Client.RequestJsonbyPostAsync(url, path, payload);
                if (res != null && res.Result.error == (int)HTTPStatusCode.Ok)
                {
                    return res.Result.response;
                }
            }
            return null;
        }

        public static string Update(string url, string path, string payload)
        {
            if (url != null)
            {
                var request = JsonConverter.Deserialize<RequestCompute>(payload);
                var response = Client.RequestJsonbyPostAsync(url, path, payload);
                if (response != null && response.Result.error == (int)HTTPStatusCode.Ok)
                {
                    return JsonConverter.Serialize(new ResponseOnlyNode { node = new Node() { nodeId = request.nodeId } });
                }
            }

            return null;
        }
        public static string Remove(string url, string path, string payload)
        {
            if (url != null)
            {
                var request = JsonConverter.Deserialize<RequestCompute>(payload);
                var response = Client.RequestJsonbyPostAsync(url, path, payload);
                if (response != null && response.Result.error == (int)HTTPStatusCode.Ok)
                {
                    return JsonConverter.Serialize(new ResponseOnlyNode { node = new Node() { nodeId = request.nodeId } });
                }
            }
            return null;
        }
    }
}
