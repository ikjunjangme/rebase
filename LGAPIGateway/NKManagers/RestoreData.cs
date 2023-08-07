using LGAPIGateway.Models;
using LGAPIGateway.Resources;
using Newtonsoft.Json;
using NKAPI.API.Model;
using NKAPI.API.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LGAPIGateway.NKManagers
{
    public class RestoreEngine
    {
        public Dictionary<string, Compute_Node_Info> Nodes { get; set; }
        public Dictionary<string, LGAPI_Analytic_Engine> Engines { get; set; }
        public Dictionary<string, NK_Analytic_Engine> EngineID_Channel_Pair { get; set; }
    }
}
