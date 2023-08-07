using Grapevine.Interfaces.Server;
using LGAPIGateway.NKManagers;
using RestAPIService.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGAPIGateway.Resources
{
    public class AuthorizationResource : ResourceBase
    {
        private AuthorizationManager _manager;
        public AuthorizationResource()
        {
            SubResources = new string[] { "auth-token" };
            _manager = new AuthorizationManager();
        }

        public override void ExcuteDELETE(IHttpContext context, string path)
        {
            base.ExcuteDELETE(context, path);
        }

        public override void ExcuteGET(IHttpContext context, string path)
        {
            base.ExcuteGET(context, path);
        }

        public override void ExcutePOST(IHttpContext context, string path) // 로그인
        {
            var auth = context.Request.Headers["Authorization"];
            string result = "";
            var succese = _manager.LoginBasic(auth, ref result);
            if(succese)
            {
                SendMessage(context, Grapevine.Shared.HttpStatusCode.Ok, "");
            }
            else
            {
                SendMessage(context, Grapevine.Shared.HttpStatusCode.BadRequest, result);
            }
        }

        public override void ExcutePUT(IHttpContext context, string path)
        {
            base.ExcutePUT(context, path);
        }
    }
}
