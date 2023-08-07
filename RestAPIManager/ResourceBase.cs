using Grapevine.Client;
using Grapevine.Interfaces.Server;
using Grapevine.Server.Attributes;
using Grapevine.Shared;
using RestAPIManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestAPIService.Resources
{
    public abstract class ResourceBase
    {
        public string[] SubResources = new string[] { "" };
        public ResourceBase()
        {
        }

        public static string RemoveFirstSegment(string segment)
        {
            var result = string.Empty;
            var nextpath = -1;
            if (segment[0] == '/')
                nextpath = segment.IndexOf('/', 1);
            else
                nextpath = segment.IndexOf('/');
            if (nextpath == -1)
            {
                result = segment;
            }
            else
            {
                nextpath++;
                result = segment.Substring(nextpath, segment.Length - nextpath);
            }

            return result;
        }

        public static string GetNTargetResource(IHttpContext context,int N)
        {
            var path = context.Request.PathInfo.Split('/');
            string result = string.Empty;
            if(path.Length > N)
            {
                result = path[N];
            }

            return result;
        }

        public static string GetNTargetResource(string path, int N)
        {
            string result = string.Empty;

            var splited = path.Split('/');
            if(splited.Length > N)
            {
                result = splited[N];
            }

            return result;
        }

        public static bool CheckNextPath(string path)
        {
            var result = false;

            if(path.IndexOf('/',1) != -1)
            {
                result = true;
            }

            return result;
        }

        protected void SendInvalidOperation(IHttpContext context)
        {
            context.Response.StatusCode = Grapevine.Shared.HttpStatusCode.BadRequest;
            context.Response.SendResponse(Encoding.UTF8.GetBytes("Not Supported Request"));
        }

        public virtual void ExcuteGET(IHttpContext context, string path)
        {
            SendInvalidOperation(context);
        }
        public virtual void ExcutePOST(IHttpContext context, string path)
        {
            SendInvalidOperation(context);
        }
        public virtual void ExcutePUT(IHttpContext context, string path)
        {
            SendInvalidOperation(context);
        }
        public virtual void ExcuteDELETE(IHttpContext context, string path)
        {
            SendInvalidOperation(context);
        }

        public virtual void SendErrorMessage(IHttpContext context, HttpStatusCode code, string message)
        {
            context.Response.StatusCode = code;
            context.Response.SendResponse(Encoding.UTF8.GetBytes(message));
        }

        public virtual void SendMessage(IHttpContext context, HttpStatusCode code, object o)
        {
            context.Response.StatusCode = code;
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.SendResponse(JSONHelper.GetUTF8ByteArrayFromeObject(o));
        }
    }
}
