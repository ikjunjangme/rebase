using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PublicUtility;

namespace LGAPIGateway.Models
{
    public class PreDefineResources : SingletonBase<PreDefineResources>
    {
        Dictionary<string, string> resources = new Dictionary<string, string>();
        string[] lines = System.IO.File.ReadAllLines("PreDefineResources.txt");

        //ikjunjang: 설정 파일로 수정
        public readonly string BaseURL;
        public readonly string SystemURL;
        public readonly string AnalyticEngineURL;
        public readonly string MetaURL;
        public readonly string AuthorizationURL;
        public readonly string LogoIcon;
        public readonly string LogoImagePath;
        public readonly string EngineManager;
        public readonly string RuleManager;
        //public readonly string NKAPIBaseURL = @"127.0.0.1";
        public readonly string NKAPIBaseURL;
        public readonly int NKAPIBasePort;
        public readonly string RequestChannelCreate;
        public readonly string RequestNodeCreate;
        public readonly string RequestChannelInfo;
        public readonly string RequestChannelDelete;
        public readonly string RequestGetAllChannel;
        public readonly string RequestNodeList;
        public readonly string RequestNodeDelete;
        public readonly string RequestEditChannel;

        public readonly string RequestVAControl;

        public readonly string RequestROICreate;
        public readonly string RequestROIEdit;
        public readonly string RequestROIDelete;

        public readonly string EventPairPath;

        public readonly int MaxEngineCount;
        public readonly int MaxRuleCount;
        public readonly int LGAPIPort;
    

        private StringBuilder _builder = new StringBuilder();

        private PreDefineResources()
        {
            foreach (string line in lines)
            {
                string[] tmp = line.Split("=");
                resources.Add(tmp[0], tmp[1]);
            }

            BaseURL = resources["BaseURL"];
            SystemURL = resources["SystemURL"];
            AnalyticEngineURL = resources["AnalyticEngineURL"];
            MetaURL = resources["MetaURL"];
            AuthorizationURL = resources["AuthorizationURL"];
            LogoIcon = resources["LogoIcon"];
            LogoImagePath = resources["LogoImagePath"];
            EngineManager = resources["EngineManager"];
            RuleManager = resources["RuleManager"];
            NKAPIBaseURL = resources["NKAPIBaseURL"];
            NKAPIBasePort = Int32.Parse(resources["NKAPIBasePort"]);
            RequestChannelCreate = resources["RequestChannelCreate"];
            RequestNodeCreate = resources["RequestNodeCreate"];
            RequestChannelInfo = resources["RequestChannelInfo"];
            RequestChannelDelete = resources["RequestChannelDelete"];
            RequestGetAllChannel = resources["RequestGetAllChannel"];
            RequestNodeList = resources["RequestNodeList"];
            RequestNodeDelete = resources["RequestNodeDelete"];
            RequestEditChannel = resources["RequestEditChannel"];
            RequestVAControl = resources["RequestVAControl"];
            RequestROICreate = resources["RequestROICreate"];
            RequestROIEdit = resources["RequestROIEdit"];
            RequestROIDelete = resources["RequestROIDelete"];
            EventPairPath = resources["EventPairPath"];
            MaxEngineCount = Int32.Parse(resources["MaxEngineCount"]);
            MaxRuleCount = Int32.Parse(resources["MaxRuleCount"]);
            LGAPIPort = Int32.Parse(resources["LGAPIPort"]);
        }

        public string CreateURL(params string[] param)
        {
            _builder.Clear();
            for (int i = 0; i < param.Length; i++)
            {
                _builder.Append("/");
                _builder.Append(param[i]);
            }
            return _builder.ToString();
        }
    }
}
