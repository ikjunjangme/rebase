using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestAPIManager.AuthorizationHelper
{
    public class BasicAuthorization : AuthorizationBase
    {
        public override bool CheckAuth(string auth)
        {
            var result = false;

            if (string.IsNullOrEmpty(auth) == false && auth.StartsWith("Basic "))
            {
                var str = Encoding.UTF8.GetString(Convert.FromBase64String(auth.Replace("Basic ", "")));
                var splitdata = str.Split(':');
                if (splitdata.Length == 2 && splitdata[0] == _CheckID && splitdata[1] == _CheckPassWord)
                {
                    result = true;
                }
            }
            return result;
        }

        public override string CreateAuthString()
        {
            if (string.IsNullOrEmpty(_LoginID) == false && string.IsNullOrEmpty(_LoginPassWord) == false)
            {
                string basestr = "Basic ";
                return basestr + Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_LoginID}:{_LoginPassWord}"));
            }
            else
            {
                return "";
            }
        }
    }
}
