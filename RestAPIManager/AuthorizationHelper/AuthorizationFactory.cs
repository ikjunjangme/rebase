using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestAPIManager.AuthorizationHelper
{
    public static class AuthorizationFactory
    {
        public static AuthorizationBase CreateAuth(string ID, string Password, AuthType type)
        {
            AuthorizationBase auth = null;
            switch (type)
            {
                case AuthType.Basic:
                    auth = new BasicAuthorization();
                    auth.SetCheckParameter(ID, Password);
                    break;
                case AuthType.Bearer:
                    break;
                default:
                    break;
            }

            return auth;
        }
    }
}
