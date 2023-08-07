using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGAPIGateway.Models
{
    public class Rule_Engine : LGAPIModelBase
    {
        public string brand { get; set; }
        public string rule_id { get; set; }
        public int rule_type { get; set; }
        public IList<int> object_type { get; set; }
        public int roi_type { get; set; }
        public IList<ROI_Positions> roi_positions { get; set; }
        public Rule_Links _links { get; set; }

        public Rule_Engine()
        {
            object_type = new List<int>();
            roi_positions = new List<ROI_Positions>();
            _links = new Rule_Links();
            _links._self = new LGAPI_Link_Base(Supported_HTTP_Method.GET | Supported_HTTP_Method.PUT | Supported_HTTP_Method.DELETE);
        }
    }

    public class Rule_Links : LGAPIModelBase
    {
        public LGAPI_Link_Base _self { get; set; }
        public LGAPI_Link_Base expansion { get; set; }
    }
}
