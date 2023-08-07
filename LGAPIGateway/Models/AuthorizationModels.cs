using PublicUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace LGAPIGateway.Models
{
    public class LGAPIToken : LGAPIModelBase
    {
        public string token { get; set; }
        public int timeout { get; set; }
    }

    public class LGAPITokenSaver : SingletonBase<LGAPITokenSaver>
    {
        private LGAPITokenSaver() { }
        private ConcurrentBag<string> _Tokens = new ConcurrentBag<string>();
        public bool AddToken(string token)
        {
            var result = false;
            _Tokens.Add(token);
            return result;
        }

        public bool RemoveToekn(string token)
        {
            var result = false;
            result = _Tokens.TryTake(out string value);
            return result;
        }
    }
}
