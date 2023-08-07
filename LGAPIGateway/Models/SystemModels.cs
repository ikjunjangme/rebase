using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

namespace LGAPIGateway.Models
{

    public class LGAPI_System : LGAPIModelBase
    {
        public string guid { get; set; }
        public string brand_name { get; set; }
        public LGAPI_ICON icon { get; set; }
        public int max_engine { get; set; }
        public int max_rule { get; set; }
        public int engine_count { get; set; }
        public LGAPI_Supported_Authorization supported_authorization { get; set; }
        public IList<LGAPI_Supported_Rules> supported_rules { get; set; }
        public LGAPI_System_Links _links { get; set; }

        public LGAPI_System()
        {
            brand_name = string.Empty;
            icon = new LGAPI_ICON();
            max_engine = 0;
            max_rule = 0;
            supported_authorization = new LGAPI_Supported_Authorization();
            supported_rules = new List<LGAPI_Supported_Rules>();
            _links = new LGAPI_System_Links();
        }
    }

    public class LGAPI_ICON : LGAPIModelBase
    {
        public string href { get; set; }
        public string image_format { get; set; }

        public LGAPI_ICON()
        {
            href = string.Empty;
            image_format = string.Empty;
        }
    }

    public class LGAPI_Supported_Authorization : LGAPIModelBase
    {
        public bool digest { get; set; }
        public bool basic { get; set; }
        public bool bearer { get; set; }

        public LGAPI_Supported_Authorization()
        {
            digest = false;
            basic = false;
            bearer = false;
        }
    }

    public class LGAPI_Supported_Rules : LGAPIModelBase
    {
        public int rule_code { get; set; }
        public string rule_name { get; set; }
        public LGAPI_ICON icon { get; set; }

        public LGAPI_Supported_Rules()
        {
            rule_code = 0;
            rule_name = string.Empty;
        }
    }

    public class LGAPI_System_Links : LGAPIModelBase
    {
        public LGAPI_Link_Base _self { get; set; }
        //public LGAPI_Link_Base? accept_token { get; set; }
        //public LGAPI_Link_Base? refresh_token { get; set; }
        public LGAPI_Link_Base analytic_engines { get; set; }

        public LGAPI_System_Links()
        {
            _self = new LGAPI_Link_Base(Supported_HTTP_Method.GET);
            analytic_engines = new LGAPI_Link_Base(Supported_HTTP_Method.GET | Supported_HTTP_Method.POST);
        }
    }

    public class LGEvent_NKEvent_Pair
    {
        public int LGEventCode { get; set; }
        public int NKEventCode { get; set; }
        public string EventName { get; set; }
        public string IConPath { get; set; }
    }
}
