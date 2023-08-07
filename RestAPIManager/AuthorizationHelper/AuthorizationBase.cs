using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestAPIManager.AuthorizationHelper
{
    public class AuthorizationBase
    {
        //Todo ID, Password를 여러 쌍으로 관리하도록 수정 필요
        protected string _LoginID;
        protected string _LoginPassWord;
        protected string _CheckID;
        protected string _CheckPassWord;

        public AuthorizationBase()
        {
        }

        public void SetLoginParameter(string ID, string Password)
        {
            _LoginID = ID;
            _LoginPassWord = Password;
        }

        public void SetCheckParameter(string ID, string Password)
        {
            _CheckID = ID;
            _CheckPassWord = Password;
        }

        public virtual bool CheckAuth(string auth)
        {
            return true;
        }

        public virtual string CreateAuthString()
        {
            return string.Empty;
        }
    }
}
