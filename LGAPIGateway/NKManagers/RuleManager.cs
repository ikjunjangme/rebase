using LGAPIGateway;
using LGAPIGateway.Models;
using Newtonsoft.Json;
using NKAPI.API.Model;
using NKAPI.API.Repuest;
using NKAPI.API.Response;
using PublicUtility.Event.Enum;
using RestAPIManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LGAPIGateway.NKManagers
{
    public class RuleManager : ManagerBase
    {
        public Dictionary<string, Rule_Engine_Info> _RulID_RuleData_Pair;
        public string _channelID;
        public string _nodeID;
        /// <summary>
        /// Meta 전송때 ROi 정보들을 전송하기 위해 반환되는 리스트
        /// </summary>
        public List<LGAPI_Rule> RuleReporter { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public Dictionary<string, Rule_Engine_Info> RulID_RuleData_Pair { get => _RulID_RuleData_Pair; }

        public RuleManager(string channelID, string nodeID)
        {
            _RulID_RuleData_Pair = new Dictionary<string, Rule_Engine_Info>();
            _restClient = new RestClient(CreateNKBaseURL());
            _channelID = channelID;
            _nodeID = nodeID;
            RuleReporter = new List<LGAPI_Rule>();
        }

        private readonly string _folderPath = AppDomain.CurrentDomain.BaseDirectory + "config";
        private readonly string _rulefilePath = AppDomain.CurrentDomain.BaseDirectory + "config\\enginlist.json";
        public void WriteRuleFile(bool isDelete)
        {
            try
            {
                var folder = new DirectoryInfo(_folderPath);
                if (folder.Exists == false)
                    folder.Create();

                var engine = new RestoreEngine
                {
                    Nodes = EngineManager._nodes,
                    Engines = EngineManager._engines,
                    EngineID_Channel_Pair = EngineManager._EngineID_Channel_Pair
                };

                string jsonString = JsonConvert.SerializeObject(engine, Formatting.Indented, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                });
                File.WriteAllText(_rulefilePath, jsonString);
            }
            catch
            {
                Console.WriteLine("Failed save to backup");
            }
        }

        public bool CreateRule(Rule_Engine Rule, ref string ResultORError)
        {
            var result = false;
            var rule = new Rule_Engine();
            rule.rule_id = CommonFuntions.CreateUID();

            if (CheckValidRequest(Rule))
            {
                CopyRuleEngine(ref rule, ref Rule);

                var Rule_Engine_Info = new Rule_Engine_Info();
                Rule_Engine_Info.LGAPI_Rule_Engine_Info = rule;
                
                var req = CreateRequestROI(rule);
                var ack = _restClient.Excute(PreDefineResources.inst.RequestROICreate, req, RestSharp.Method.Post).Result.Content;

                Rule_Engine_Info.ROI_Info = new ResponseRoiInfo();
                _RulID_RuleData_Pair.Add(rule.rule_id, Rule_Engine_Info);

                ResultORError = rule.rule_id;

                var NewRule = new LGAPI_Rule();

                NewRule.id = rule.rule_id;
                NewRule.type = rule.rule_type;
                NewRule.roi_type = rule.roi_type;
                NewRule.roi = new List<float>();
                foreach (var item in rule.roi_positions)
                {
                    NewRule.roi.Add(item.x);
                    NewRule.roi.Add(item.y);
                }
                RuleReporter.Add(NewRule);
                result = true;

                WriteRuleFile(false);
                Console.WriteLine("Rule Create Success");
            }
            else
            {
                ResultORError = "Rule Create Message Invalid";
                Console.WriteLine("Rule Create Failed");
            }
            return result;
        }

        public bool RemoveRule(string ID)
        {
            var result = false;
            if(_RulID_RuleData_Pair.ContainsKey(ID))
            {
                var req = CreateRequestROI();
                req.roiIds = new List<string>() { _RulID_RuleData_Pair[ID].LGAPI_Rule_Engine_Info.rule_id };
                var ack = JSONHelper.GetObjectFromJSONString<ResponseOnlyNode>(_restClient.Excute(PreDefineResources.inst.RequestROIDelete, req, RestSharp.Method.Post).Result.Content);
                _RulID_RuleData_Pair.Remove(ID);
                result = true;
                RuleReporter.Remove(RuleReporter.Where(o => { return o.id == ID; }).First());
                WriteRuleFile(true);
                Console.WriteLine("Rule Remove Success");
            }
            return result;
        }

        public bool EditRule(Rule_Engine rule,string target)
        {
            var result = false;
            _RulID_RuleData_Pair.TryGetValue(target, out var value);
            if(value == null)
            {
                return result;
            }
            var origin = value.LGAPI_Rule_Engine_Info;
            CopyRuleEngine(ref origin, ref rule);
            value.LGAPI_Rule_Engine_Info = origin;
            var req = CreateRequestROI(origin);
            var ack = JSONHelper.GetObjectFromJSONString<ResponseOnlyNode>(_restClient.Excute(PreDefineResources.inst.RequestROIEdit, req, RestSharp.Method.Post).Result.Content);
            if(ack.code == 0)
            {
                result = true;
            }
            var NewRule = new LGAPI_Rule();

            NewRule.id = origin.rule_id;
            NewRule.type = rule.rule_type;
            NewRule.roi_type = rule.roi_type;
            NewRule.roi = new List<float>();
            foreach (var item in rule.roi_positions)
            {
                NewRule.roi.Add(item.x);
                NewRule.roi.Add(item.y);
            }

            RuleReporter.RemoveAll(x => x.id == target);
            RuleReporter.Add(NewRule);
            WriteRuleFile(false);
            Console.WriteLine("Rule Edit Success");
            return result;
        }

        private void CopyRuleEngine(ref Rule_Engine target, ref Rule_Engine source)
        {
            target.roi_type = source.roi_type != 0 ? source.roi_type : target.roi_type;
            target.rule_type = source.rule_type != 0 ? source.rule_type : target.rule_type;
            target.object_type = new List<int>(source.object_type ??= target.object_type);
            target.roi_positions = new List<ROI_Positions>(source.roi_positions ??= target.roi_positions);
        }

        public ObjectType GetNKObjectEnumFromeLGEnum(LG_Object_Type type)
        {
            var result = ObjectType.PERSON;
            if(Enum.TryParse<ObjectType>(type.ToString(),out result) == false)
            {
                result = ObjectType.NOT_DEFINE;
            }

            return result;
        }


        public RequestRoi CreateRequestROI()
        {
            var req = new RequestRoi();
            req.channelId = _channelID;
            req.nodeId = _nodeID;
            return req;
        }

        public RequestRoi CreateRequestROI(Rule_Engine rule)
        {
            var req = CreateRequestROI();
            req.eventType = CommonFuntions.GetNKEventFromLGEvent((LG_Event_Type)rule.rule_type);
            req.roiDots = new List<NKAPI.API.Model.RoiDot>();
            req.roiId = rule.rule_id;
            req.roiIds = new List<string>();
            req.roiIds.Add(rule.rule_id);
            foreach (var item in rule.roi_positions)
            {
                req.roiDots.Add(new NKAPI.API.Model.RoiDot() { x = item.x, y = item.y });
            }
            req.feature = RoiFeature.ALL;
            var filter = new PublicUtility.Event.Class.EventFiltering() { maxDetectSize = 100, minDetectSize = 1, objectsTarget = new List<ObjectType>() };
            foreach (var item in rule.object_type)
            {
                filter.objectsTarget.Add(CommonFuntions.GetNKObjectEnumFromeLGEnum((LG_Object_Type)item));
            }

            if (req.eventType == EventType.EVT_FIRE_FLAME)
            {
                filter.objectsTarget.Add(ObjectType.FLAME);
            }
            if (req.eventType == EventType.EVT_FIRE_SMOKE)
            {
                filter.objectsTarget.Add(ObjectType.SMOKE);
            }
            if (req.eventType == EventType.EVT_FACE_MASKED || req.eventType == EventType.EVT_FACE_MATCHING)
            {
                filter.objectsTarget.Add(ObjectType.FACE_MAN);
            }
            if (req.eventType == EventType.EVT_HEAD_HELMET || req.eventType == EventType.EVT_HEAD_NO_HELMET)
            {
                filter.objectsTarget.Add(ObjectType.FACE_HELMET);
                filter.objectsTarget.Add(ObjectType.FACE_HEAD);
            }


            req.eventFilter = filter;

            return req;
        }

        public List<LGAPI_Rule> ReportRule()
        {
            var result = new List<LGAPI_Rule>();

            return result;
        }

        private bool CheckValidRequest(Rule_Engine engine)
        {
            if(engine != null && engine.object_type != null && engine.roi_positions != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
