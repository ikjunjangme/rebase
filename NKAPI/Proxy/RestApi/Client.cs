using System;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using NKAPI.API;
using PublicUtility.API;
using PublicUtility.HTTP;

namespace NKAPI.Proxy.RestApi
{
    public class Client
    {

        public readonly string baseUri;
        public readonly string Host;
        public readonly int Port;
        public readonly APIVersion APIVersion;
        private readonly int _timeOut;

        public Client(string ip, int port, APIVersion ver = APIVersion.v2, int timeout = 3)
        {
            Host = ip;
            Port = port;
            APIVersion = ver;
            baseUri = $"http://{ip}:{port}";
            _timeOut = timeout;
        }
        public static string SendPost(string url, string json)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    using (var content = new StringContent(json, Encoding.UTF8, "application/json"))
                    {
                        HttpResponseMessage messge = client.PostAsync(url, content).Result;
                        if (messge.IsSuccessStatusCode)
                        {
                            return messge.Content.ReadAsStringAsync().Result;
                        }
                    }
                }
            }
            catch
            {
            }

            return "";
        }

        public static (string response, int error) SendPing(string uri, string path)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string header = "application/json";
                    client.BaseAddress = new Uri(uri);
                    client.Timeout = new TimeSpan(0, 0, 0, 0, 1);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(header));
                    HttpContent content = new StringContent("{}", Encoding.UTF8, header);
                    HttpResponseMessage messge = client.PostAsync(path, content).Result;
                    if (messge.IsSuccessStatusCode)
                    {
                        return (messge.Content.ReadAsStringAsync().Result, (int)HTTPStatusCode.Ok);
                    }
                }
            }
            catch
            {
                return ("NotFound http server", (int)HTTPStatusCode.NotFound);
            }
            return (null, (int)HTTPStatusCode.RequestTimeout);
        }

        public static Task<(string response, int error)> KeeepAlive(string uri, string path)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string header = "application/json";
                    client.BaseAddress = new Uri(uri);
                    client.Timeout = new TimeSpan(0, 0, 0, 3);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(header));
                    System.Net.Http.HttpContent content = new StringContent("{}", Encoding.UTF8, header);
                    HttpResponseMessage messge = client.PostAsync(path, content).Result;
                    if (messge.IsSuccessStatusCode)
                    {
                        return Task.FromResult((messge.Content.ReadAsStringAsync().Result, (int)HTTPStatusCode.Ok));
                    }
                }
            }
            catch
            {
                return Task.FromResult(("NotFound http server", (int)HTTPStatusCode.NotFound));
            }
            return Task.FromResult(("", (int)HTTPStatusCode.RequestTimeout));
        }
        public static Task<(string response, int error)> RequestJsonbyPostAsync(string uri ,string path, string json)
        {
            string msg = "NotFound http server";
            try
            {
                using (var client = new HttpClient())
                {
                    string header = "application/json";
                    client.BaseAddress = new Uri(uri);
                    client.Timeout = new TimeSpan(0, 0, 0, 3);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(header));
                    HttpContent content = new StringContent(json, Encoding.UTF8, header);
                    HttpResponseMessage messge = client.PostAsync(path, content).Result;
                    msg = messge.Content.ReadAsStringAsync().Result;
                    if (messge.IsSuccessStatusCode)
                    {
                        return Task.FromResult((msg, (int)HTTPStatusCode.Ok));
                    }
                }
            }
            catch
            {
                return Task.FromResult((msg, (int)HTTPStatusCode.NotFound));
            }
            return Task.FromResult((msg, (int)HTTPStatusCode.RequestTimeout));
        }
    }
}
