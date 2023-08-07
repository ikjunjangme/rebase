using NKAPI.API.Model;
using NKAPI.API.Repuest;
using NKAPI.API.Response;
using NKAPI.Proxy.RestApi;
using PublicUtility.HTTP;
using System.Collections.Generic;
using JsonConverter = PublicUtility.Converters.JsonConverter;

namespace NKAPI.API.v2
{
    public partial class Channel
    {
        public static string Regist(string url, string path, string payload)
        {
            var res = Client.RequestJsonbyPostAsync(url, path, payload);
            if (res.IsCompleted && res.Result.error == (int)HTTPStatusCode.Ok)
            {
                return res.Result.response;
            }

            return JsonConverter.Serialize(new ResponseOnlyNode
            {
                code = 404,
                message = "Not Found NodeId"
            });
        }
        public static string Snapshot(string url, string path, string payload)
        {
            ResponseChannel response = new ResponseChannel
            {
                imageData = "jpegimage443634123asdasd12d21d12d1,...",
                code = 0
            };

            return JsonConverter.Serialize(response);
        }
        public static string SetCalibration(string url, string path, string payload)
        {
            ResponseChannel response = new ResponseChannel
            {
                channelId = "asdasdasd",
                nodeId = "aaa"
            };

            return JsonConverter.Serialize(response);
        }
    }
}
