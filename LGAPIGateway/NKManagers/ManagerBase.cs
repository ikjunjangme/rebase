using LGAPIGateway.Models;
using NKAPI.Proxy.RestApi;
using RestAPIManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGAPIGateway.NKManagers
{
    public class ManagerBase
    {
        protected RestClient _restClient;
        public ManagerBase()
        {
        }

        public static string CreateURL(string ip,int port, string path)
        {
            return $"http://{ip}:{port}{path}";
        }

        public static string CreateNKBaseURL()
        {
            return $"http://{PreDefineResources.inst.NKAPIBaseURL}:{PreDefineResources.inst.NKAPIBasePort}";
        }


    }
}
