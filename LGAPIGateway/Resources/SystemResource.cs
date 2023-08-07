using Grapevine.Interfaces.Server;
using Grapevine.Server.Attributes;
using LGAPIGateway;
using LGAPIGateway.Models;
using LGAPIGateway.Singletons;
using Newtonsoft.Json;
using NKAPI.Proxy.RestApi;
using RestAPIManager;
using RestAPIService.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LGAPIGateway.Resources
{
    public class SystemResource : ResourceBase
    {
        private int _EngineCount;
        private string _GUID;
        private Dictionary<string, string> _ResourceName_Path_Pair = new Dictionary<string, string>();
        private LGAPI_System _System_Response_Cache;

        private readonly string _filePath = AppDomain.CurrentDomain.BaseDirectory + "config\\systemDB.json";
        private readonly string _folderPath = AppDomain.CurrentDomain.BaseDirectory + "config";

        public SystemResource()
        {
            SubResources = new string[] { "system" };
            _ResourceName_Path_Pair.Add("Logo", PreDefineResources.inst.LogoIcon);
            //_System_Response_Cache = ReadFile();

            if(_System_Response_Cache == null)
                _GUID = CommonFuntions.CreateUID();
        }

        public bool WriteFile()
        {
            try
            {
                if (_System_Response_Cache != null)
                {
                    var folder = new DirectoryInfo(_folderPath);
                    if (folder.Exists == false)
                        folder.Create();

                    string jsonString = JsonConvert.SerializeObject(_System_Response_Cache, Formatting.Indented, new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });
                    File.WriteAllText(_filePath, jsonString);
                    return true;
                }
            }
            catch
            {
                Console.WriteLine("Failed save to backup");
            }
            return false;
        }

        public LGAPI_System ReadFile()
        {
            try
            {
                var folder = new DirectoryInfo(_folderPath);
                if (folder.Exists == true)
                    if (File.Exists(_filePath))
                    {
                        string json = File.ReadAllText(_filePath);
                        var lg_system = JsonConvert.DeserializeObject<LGAPI_System>(json);
                        _EngineCount = lg_system.engine_count;
                        _GUID = lg_system.guid;
                        return lg_system;
                    }
            }
            catch
            {
                Console.WriteLine("Failed load to backup");
            }
            return null;
        }

        public void GetEngineCount(int count)
        {
            _EngineCount = count;
        }

        ///System Resource에 대한 조회 처리
        ///AnalyticEngineManager와 Authorization 경로 전송
        public override void ExcuteGET(IHttpContext context, string path)
        {
            if (ResourceBase.CheckNextPath(path)) // SubTarget Check SubTarget이 없으면 system 자신에게 온 요청으로 처리
            {
                if (_ResourceName_Path_Pair.ContainsKey(path))
                {
                    SendIconImage(context, _ResourceName_Path_Pair[path]);
                }
                else
                {
                    //SendErrorMessage()
                }
            }
            else
            {
                if (_System_Response_Cache == null)
                {
                    _System_Response_Cache = new LGAPI_System();
                    _System_Response_Cache.guid = _GUID;
                    _System_Response_Cache.brand_name = "NextK";

                    _System_Response_Cache.icon.href = PreDefineResources.inst.CreateURL(PreDefineResources.inst.SystemURL, PreDefineResources.inst.LogoIcon);
                    _System_Response_Cache.icon.image_format = "PNG";

                    _System_Response_Cache.max_engine = PreDefineResources.inst.MaxEngineCount;
                    _System_Response_Cache.max_rule = PreDefineResources.inst.MaxRuleCount;

                    _System_Response_Cache.supported_authorization.basic = true;

                    _System_Response_Cache.supported_rules = new List<LGAPI_Supported_Rules>();

                    if (GlobalConfigs.inst.Event_Pair != null)
                    {
                        foreach (var item in GlobalConfigs.inst.Event_Pair)
                        {
                            var rule = new LGAPI_Supported_Rules();
                            rule.rule_code = item.LGEventCode;
                            rule.rule_name = item.EventName;

                            if (string.IsNullOrEmpty(item.IConPath) == false)
                            {
                                rule.icon = new LGAPI_ICON();
                                rule.icon.image_format = Path.GetExtension(item.IConPath);
                                rule.icon.href = PreDefineResources.inst.CreateURL(PreDefineResources.inst.SystemURL, item.LGEventCode.ToString());
                                //Todo _ResourceName_Path_Pair에 아이콘 이름 추가 필요
                                //현재는 _ResourceName_Path_Pair에 아이콘 이름이 없어서 아이콘의 주소로 오는 요청을 모두 거절함
                            }

                            _System_Response_Cache.supported_rules.Add(rule);
                        }
                    }

                    _System_Response_Cache._links._self.href = PreDefineResources.inst.CreateURL(PreDefineResources.inst.BaseURL, PreDefineResources.inst.SystemURL);
                    _System_Response_Cache._links.analytic_engines.href = PreDefineResources.inst.CreateURL(PreDefineResources.inst.BaseURL, PreDefineResources.inst.AnalyticEngineURL, PreDefineResources.inst.EngineManager);
                }

                _System_Response_Cache.engine_count = _EngineCount;


                //if(_EngineCount > 0)
                //{
                //    WriteFile();
                //}
                //else
                //{
                //    _System_Response_Cache = _System_Response_Cache.ReadFile();
                //}

                SendMessage(context, Grapevine.Shared.HttpStatusCode.Ok, _System_Response_Cache);
            }
        }

        private void SendIconImage(IHttpContext context, string path)
        {
            //ytgu 테스트 데이터 사용
            //Todo 로고 이미지 구해서 사용 필요
            using (var fs = new FileStream(path, FileMode.Open))
            {
                var buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                context.Response.SendResponse(buffer);
            }
        }
    }
}
