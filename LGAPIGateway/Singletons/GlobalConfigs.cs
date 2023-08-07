using LGAPIGateway.Models;
using PublicUtility;
using RestAPIManager;
using System.Collections.Generic;

namespace LGAPIGateway.Singletons
{
    public class GlobalConfigs : SingletonBase<GlobalConfigs>
    {
        public List<LGEvent_NKEvent_Pair> Event_Pair;

        private GlobalConfigs()
        {
            Init();
        }

        private void Init()
        {
            Event_Pair = JSONHelper.GetObjectFromJSONString<List<LGEvent_NKEvent_Pair>>(CommonFuntions.LoadJson(PreDefineResources.inst.EventPairPath));
        }
    }
}
