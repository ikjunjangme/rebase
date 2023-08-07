using LGAPIGateway.NKManagers;
using LGAPIGateway.Resources;
using NKAPI.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGAPIGateway.Models
{
    public class LGAPI_Analytic_Engine : LGAPIModelBase
    {
        public bool enable { get; set; }
        public string engine_id { get; set; }
        public string engine_name { get; set; }
        public int sensitivity { get; set; }
        public int fps { get; set; }
        public LGAPI_Video video { get; set; }
        public LGAPI_Meta_Link meta { get; set; } = new LGAPI_Meta_Link();
        public string grpcURL { get; set; }
        public LGAPI_AnalyticEngineLinks _links { get; set; }
    }

    public class LGAPI_Video : LGAPIModelBase
    {
        public string url { get; set; }
        public string login_id { get; set; }
        public string login_pwd { get; set; }

        public LGAPI_Video()
        {
            url = string.Empty;
            login_id = string.Empty;
            login_pwd = string.Empty;
        }
    }

    public class LGAPI_Meta_Link : LGAPIModelBase
    {
        public string url { get; set; }
        public string login_id { get; set; }
        public string login_pwd { get; set; }
        public IList<int> filters { get; set; }

        public LGAPI_Meta_Link()
        {
            url = string.Empty;
            login_id = string.Empty;
            login_pwd = string.Empty;
            filters = new List<int>();
        }
    }

    public class LGAPI_AnalyticEngineLinks : LGAPIModelBase
    {
        public LGAPI_Link_Base _self { get; set; }
        public LGAPI_Link_Base rule_engines { get; set; }

        public LGAPI_AnalyticEngineLinks()
        {
            _self = new LGAPI_Link_Base(Supported_HTTP_Method.GET | Supported_HTTP_Method.DELETE | Supported_HTTP_Method.PUT);
            rule_engines = new LGAPI_Link_Base(Supported_HTTP_Method.GET | Supported_HTTP_Method.POST);
        }
    }

    public class NK_Analytic_Engine : LGAPIModelBase
    {
        public ChannelInfo Channel_Information { get; set; }
        public Analytic_Engine_Instance Engine_Instance { get; set; }
        public RuleEngineResource RuleEngineResource { get; set; }
        public MetaManager MetaManager { get; set; }

        public string GRPCAddr { get; set; }

        public NK_Analytic_Engine()
        {
            Channel_Information = new ChannelInfo();
            Engine_Instance = new Analytic_Engine_Instance();
            RuleEngineResource = new RuleEngineResource();
        }
    }
}
