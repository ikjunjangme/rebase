using Newtonsoft.Json;
using NKAPIService.API;
using NKAPIService.API.Channel;
using NKAPIService.API.ComputingNode;
using NKAPIService.API.VideoAnalysisSetting;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NKAPIService
{
    public class APIService
    {
        private HttpBasicAuthenticator _authenticator;
        public Uri BaseUrl { get; private set; }
        private RestClient _restClient;

        private APIService()
        {

        }

        public override bool Equals(object obj)
        {
            if (obj is APIService service)
            {
                return this.BaseUrl == service.BaseUrl;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }


        public static APIService Build()
        {
            return new APIService();
        }

        public APIService SetBasicAuthenticator(string userName, string userPassword)
        {
            _authenticator = new HttpBasicAuthenticator(userName, userPassword);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"> full uri</param>
        public APIService SetUrl(Uri baseUrl)
        {
            BaseUrl = baseUrl;
            _restClient = new RestClient(BaseUrl);
            if (_authenticator != null)
                _restClient.Authenticator = _authenticator;

            return this;
        }


        public APIService SetClient(RestClient restClient)
        {
            _restClient = restClient;

            return this;
        }


        private readonly Dictionary<RequestType, Func<string, ResponseBase>> _parsingMap = new Dictionary<RequestType, Func<string, ResponseBase>>()
        {
            #region CN
		    { RequestType.CreateComputingNode, json=> JsonConvert.DeserializeObject<ResponseCreateComputingNode>(json) },
            { RequestType.UpdateComputingNode, json=> JsonConvert.DeserializeObject<ResponseUpdateComputingNode>(json) },
            { RequestType.RemoveComputingNode, json=> JsonConvert.DeserializeObject<ResponseRemoveComputingNode>(json) },
            { RequestType.GetComputingNode, json=>JsonConvert.DeserializeObject<ResponseGetComputingNode>(json) },
            { RequestType.ListComputingNode, json=>  JsonConvert.DeserializeObject<ResponseListComputingNode>(json)},
	        #endregion  
            
            #region Channel
		    { RequestType.GetChannel, json => JsonConvert.DeserializeObject<ResponseGetChannel>(json)},
            { RequestType.ListChannel, json => JsonConvert.DeserializeObject<ResponseListChannels>(json)},
            { RequestType.RegisterChannel, json => JsonConvert.DeserializeObject<ResponseRegisterChannel>(json) },
            { RequestType.RemoveChannel, json => JsonConvert.DeserializeObject<ResponseRemoveChannel>(json) },
            { RequestType.UpdateChannel, json => JsonConvert.DeserializeObject<ResponseBase>(json) }, 
	        #endregion

            #region Events
		    { RequestType.CreateROI, json => JsonConvert.DeserializeObject<ResponseCreateROI>(json) },
            { RequestType.RemoveROI, json => JsonConvert.DeserializeObject<ResponseBase>(json) },
            { RequestType.UpdateROI, json => JsonConvert.DeserializeObject<ResponseBase>(json) },
            { RequestType.GetROI, json => JsonConvert.DeserializeObject<ResponseGetROI>(json) },
            { RequestType.ListROI, json => JsonConvert.DeserializeObject<ResponseListROI>(json) },
            { RequestType.Control, json => JsonConvert.DeserializeObject<ResponseControl>(json) },
            
	        #endregion

            #region FaceDB
            { RequestType.RegisterFaceDB, json => JsonConvert.DeserializeObject<ResponseRegisterFaceDB>(json) },
            { RequestType.UpdateFaceDB, json => JsonConvert.DeserializeObject<ResponseUpdateFaceDB>(json) },
            { RequestType.UnRegisterFaceDB, json => JsonConvert.DeserializeObject<ResponseUnRegisterFaceDB>(json) }, 
	        #endregion

            #region Etc
		    { RequestType.Snapshot, json => JsonConvert.DeserializeObject<ResponseSnapshot>(json) },
            { RequestType.Calibrate, json => JsonConvert.DeserializeObject<ResponseCalibrate>(json)}, 
	        #endregion
        };

        public async Task<ResponseBase> Requset(IRequset requestBody, TimeSpan timeSpan)
        {
            ResponseBase rb = new ResponseBase() { Code = ErrorCodes.RequsetTimeout };
            var req = new RestRequest()
            {
                Resource = requestBody.GetResource(),
                Method = Method.POST,
                RequestFormat = DataFormat.Json,
            };

            req.AddHeader("Content-type", "application/json");
            req.AddJsonBody(requestBody);

            await Task.Run(() =>
            {
                var post = _restClient.ExecutePostAsync(req);

                if (post.Wait(timeSpan))
                {
                    var res = post.Result;
                    if (res.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        rb = _parsingMap[requestBody.RequsetType](res.Content);
                    }
                }
            });

            return rb;
        }
        public async Task<ResponseBase> Requset(IRequset requestBody)
        {
            TimeSpan timeSpan = new TimeSpan(0, 0, 3);
            return await Requset(requestBody, timeSpan);
        }

        public async Task<bool> IsAnyAG()
        {
            if (_restClient == null) throw new ArgumentNullException();

            var responseBase = await Requset(new RequestListComputingNode());

            return responseBase != null && responseBase.Code == ErrorCodes.Success;
        }
    }
}
