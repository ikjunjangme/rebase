using LGAPIGateway.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGAPIGateway.NKManagers
{
    public class AuthorizationManager : ManagerBase
    {
        private LGAPITokenSaver _tokenSaver;
        private Dictionary<string, string> _Acount_Password_Pair;

        public AuthorizationManager()
        {
            _tokenSaver = LGAPITokenSaver.inst;
            _Acount_Password_Pair = new Dictionary<string, string>();
            InitAcounts();
        }

        public bool LoginBasic(string base64data, ref string resultstring)
        {
            var result = false;
            
            if (string.IsNullOrEmpty(base64data) == false && base64data.StartsWith("Basic "))
            {
                var str = Encoding.UTF8.GetString(Convert.FromBase64String(base64data.Replace("Basic ", "")));
                var splitdata = str.Split(':');
                if (splitdata.Length == 2 && 
                    _Acount_Password_Pair.TryGetValue(splitdata[0], out var value) && 
                    value == splitdata[1])
                {
                    resultstring = "OK";
                    result = true;
                }
                else
                {
                    resultstring = "Password or ID Invalid";
                }
            }
            else
            {
                resultstring = "Invalid String";
            }

            return result;
        }

        private void InitAcounts()
        {
            _Acount_Password_Pair.Add("admin", "12345");
        }
    }
}
