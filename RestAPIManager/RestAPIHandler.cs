using Grapevine.Interfaces.Server;
using Grapevine.Server;
using Grapevine.Shared;
using RestAPIService.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using PublicUtility.Logger;
using RestAPIManager.AuthorizationHelper;

namespace RestAPIService
{
    public class RestAPIHandler
    {
        private IRestServer _server;
        private List<ResourceBase> _resources = new List<ResourceBase>();
        private bool _isInit = false;
        private WriteLog _Logger;
        private AuthorizationBase _auth;
        public string RestBaseURL { get; }

        public RestAPIHandler(string BaseURL = "uplus-vaapi")
        {
            RestBaseURL = BaseURL;
            _Logger = new WriteLog(this.GetType(), this.GetType().FullName);
        }

        public void Init(int port)
        {
            if (_isInit == false)
            {
                _server = new RestServer { Host = "*", Port = port.ToString() };
                _server.Router.Register(OnRecvRequest, HttpMethod.ALL);
                _isInit = true;
                WriteBaseLog($"Open Server : {_server.Host}:{_server.Port}");
            }
        }

        public void EnableAuth(string ID, string passwd, AuthType type)
        {
            _auth = AuthorizationFactory.CreateAuth(ID, passwd, type);
        }

        public void AddResources(ResourceBase res)
        {
            if (res != null)
                _resources.Add(res);
        }

        public void Start()
        {
            if (_server.IsListening == false)
            {
                WriteBaseLog("===Server Start===");
                _server.Start();
            }
        }

        private string GetTargetResource(IHttpContext context)
        {
            var path = context.Request.PathInfo.Split('/');
            if (path.Length < 2)
            {
                return null;
            }
            else
            {
                return path[1];
            }
        }

        private IHttpContext OnRecvRequest(IHttpContext context)
        {
            var path = GetTargetResource(context);
            if(_auth != null)
            {
                var authstr = context.Request.Headers["Authorization"];
                if(_auth.CheckAuth(authstr) == false)
                {
                    context.Response.StatusCode = HttpStatusCode.Unauthorized;
                    context.Response.SendResponse("");
                    WriteBaseLog("Authorization Failed");
                    return context;
                }
            }
            WriteBaseLog($"Request Method : {context.Request.HttpMethod}, Path : {context.Request.PathInfo}");
            if (string.Compare(path, RestBaseURL) == 0)
            {
                path = ResourceBase.RemoveFirstSegment(context.Request.PathInfo);
                var target = ResourceBase.GetNTargetResource(path, 0);
                foreach (var item in _resources)
                {
                    if (item.SubResources.Contains(target) && string.IsNullOrEmpty(path) == false)
                    {
                        path = ResourceBase.RemoveFirstSegment(path);
                        switch (context.Request.HttpMethod)
                        {
                            case HttpMethod.DELETE:
                                item.ExcuteDELETE(context, path);
                                break;
                            case HttpMethod.GET:
                                item.ExcuteGET(context, path);
                                break;
                            case HttpMethod.POST:
                                item.ExcutePOST(context, path);
                                break;
                            case HttpMethod.PUT:
                                item.ExcutePUT(context, path);
                                break;
                            default:
                                context.Response.StatusCode = HttpStatusCode.BadRequest;
                                context.Response.SendResponse(RestAPIManager.JSONHelper.GetUTF8ByteArrayFromeObject("Not Supported Request"));
                                break;
                        }
                    }
                }
            }
            else
            {
                context.Response.StatusCode = HttpStatusCode.BadRequest;
                context.Response.SendResponse(RestAPIManager.JSONHelper.GetUTF8ByteArrayFromeObject("Not Supported Path"));
            }
            return context;
        }

        private void WriteBaseLog(string log)
        {
            Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd:HH.mm.ss")} : {log}");
        }
    }
}
