using System;
using Grapevine.Shared;
using Grapevine.Server;
using Grapevine.Server.Attributes;
using System.Net;
using System.Net.Sockets;
using Grapevine.Interfaces.Server;
using NKLogger = PublicUtility.Logger.WriteLog;
using PublicUtility.API;

namespace NKAPI.Proxy.RestApi
{
    public class Server : RestResource
    {
        private readonly NKLogger _nklog = new NKLogger(typeof(Server), "APIGatewayLog");
        private IRestServer _server;
        public event EventHandler<IHttpContext> RequestPostMethod;
        public int Port;
        public APIVersion Version;
        public Server(int port, APIVersion version)
        {
            Version = version;
            string confirmPort = PortFinder.FindNextLocalOpenPort(port);
            Port = Convert.ToInt32(confirmPort);
            _server = new RestServer { Host = "*", Port = confirmPort };
        }
        public bool IsListening()
        {
            if(_server != null)
            {
                return _server.IsListening;
            }
            return false;
        }
        public void Start()
        {
            if (_server.IsListening == false)
            {
                _server.Router.Register(ReceivedPOSTMethod, HttpMethod.POST);
                _server.Router.Register(ReceivedOtherMethod, HttpMethod.ALL); //나머지 요청에 대해서는 FAIL 처리
                _server.Start();
            }
        }
        public void Stop()
        {
            if (_server.IsListening)
            {
                _server.Stop();
            }
        }
        private IHttpContext ReceivedPOSTMethod(IHttpContext context)
        {
            RequestPostMethod?.Invoke(this, context);
            return context;
        }
        //POST 이외는 안씀.
        private IHttpContext ReceivedOtherMethod(IHttpContext context)
        {
            context.Response.SendResponse(Grapevine.Shared.HttpStatusCode.MethodNotAllowed, "");
            return context;
        }
        public bool SendResponse(IHttpContext context, int httpStatusCode, string response)
        {
            try
            {
                context.Response.SendResponse((Grapevine.Shared.HttpStatusCode)httpStatusCode, response);
                return true;
            }
            catch(Exception e)
            {
                _nklog.Warn($"Conflict SendResponse : {e}");
            }
            return false;
        }

        public string GetAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return "emptyNetwork";
        }
    }
}
