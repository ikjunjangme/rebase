using NKAPI.Proxy.RestApi;
using PublicUtility.HTTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKAPI.API.POST
{
    public class SendPost
    {
        public static string PostAsync(string url, string path, string payload)
        {
            if (url != null)
            {
                var res = Client.RequestJsonbyPostAsync(url, path, payload);
                if (res.IsCompleted && res.Result.error == (int)HTTPStatusCode.Ok)
                {
                    return res.Result.response;
                }
            }
            return null;
        }
    }
}
