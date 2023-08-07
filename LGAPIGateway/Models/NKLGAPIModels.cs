using LGAPIGateway.Models;
using NKAPI.API.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGAPIGateway.Models
{
    /// <summary>
    /// NK와 LGAPI 사이의 연동을 위해 필요한 모델들
    /// </summary>
    
    public class Analytic_Engine_Instance
    {
        public string NodeID;
        public string ChannelID;
        public string Engine_ID { get; set; }
    }

    public class Compute_Node_Info
    {
        public ResponseCompute NodeInfo { get; set; }
        public List<string> Channels { get; set; }

        public Compute_Node_Info(ResponseCompute info)
        {
            NodeInfo = info;
            Channels = new List<string>();
        }
    }

    public class Rule_Engine_Info
    {
        public ResponseRoiInfo ROI_Info { get; set; }
        public Rule_Engine LGAPI_Rule_Engine_Info { get; set; }

        public Rule_Engine_Info()
        {
            ROI_Info = new ResponseRoiInfo();
            LGAPI_Rule_Engine_Info = new Rule_Engine();
        }
    }


}
