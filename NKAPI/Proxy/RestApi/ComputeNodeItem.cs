using System;
using System.Collections.Generic;
using System.Text;

namespace NKAPI.Proxy.RestApi
{
    public class ComputeNodeItem
    {
        public string baseUri {
            get
            {
                return $"http://{host}:{httpPort}";
            }
        }
        public string nodeId { get; set; }
        public string nodeName { get; set; }
        public string host { get; set; }
        public string rpcHost { get; set; }
        public int httpPort { get; set; }
        public int rpcPort { get; set; }
        public DateTime dateTime { get; set; }
    }
}
