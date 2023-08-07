using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RestAPIManager.AuthorizationHelper;
using RestSharp;

namespace RestAPIManager
{
    public class RestClient
    {
        private RestSharp.RestClient _restClient = null;
        private AuthorizationBase _Auth;

        public RestClient(string baseURL)
        {
            _restClient = new RestSharp.RestClient(baseURL);
        }

        public async Task<RestResponse> Excute(string path,object data,Method meth, int timeout = 0)
        {
            RestRequest req = new RestRequest();
            req.Method = meth;
            req.Resource = path;
            req.RequestFormat = DataFormat.Json;
            if(_Auth != null)
            {
                var str = _Auth.CreateAuthString();
                req.AddHeader("Authorization", str);
            }
            if(timeout > 0)
            {
                req.Timeout = timeout;
            }
            req.AddHeader("Content-Type", "application/json");
            req.AddJsonBody(data);
            RestResponse response = null;
            try
            {
                response = await _restClient.ExecuteAsync(req);
            }
            catch (Exception ex)
            {
                
            }
            return response;
        }

        public void EnableAuth(string id, string passwd, AuthType type)
        {
            _Auth = new BasicAuthorization();
            _Auth.SetLoginParameter(id, passwd);
        }

    }
}
