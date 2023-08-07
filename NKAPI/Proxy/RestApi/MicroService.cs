using System;
using System.Collections.Generic;
using System.Text;

namespace NKAPI.Proxy.RestApi
{
    public class MicroService
    {
        public string NodeId { get; set; }
        public string NodeName { get; set; }
        public List<ChannelObject> Channels { get; set; }
        public string Host { get; set; }
        public int RestApiPort { get; set; }
        public int GrpcPort { get; set; }
    }
}
