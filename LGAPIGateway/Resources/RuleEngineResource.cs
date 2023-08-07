using Grapevine.Interfaces.Server;
using LGAPIGateway.Models;
using LGAPIGateway.NKManagers;
using Newtonsoft.Json;
using RestAPIManager;
using RestAPIService.Resources;
using System;
using System.Collections.Generic;
using System.IO;

namespace LGAPIGateway.Resources
{
    public class RuleEngineResource : ResourceBase
    {
        public RuleManager _ruleManager;
        private string _ChannelID;
        private string _NodeID;
        private string _BaseURL;
        
        public RuleEngineResource()
        {
        }

        public RuleManager Init(Analytic_Engine_Instance engine, string BaseURL)
        {
            _ruleManager = new RuleManager(engine.ChannelID, engine.NodeID);
            _ChannelID = engine.ChannelID;
            _NodeID = engine.NodeID;
            _BaseURL = BaseURL;
            return _ruleManager;
        }

        ///RuleEngine에 대한 조회 처리
        ///RuleEngine Manager에 대한 요청이면 RuleManager를 통해서 모든 룰에 대한 정보를 전송
        ///특정 Rule에 대한 경로로 요청이면 해당하는 룰에 대해서만 전송
        public override void ExcuteGET(IHttpContext context, string path)
        {
            var subtarget = RemoveFirstSegment(path);
            if (string.IsNullOrEmpty(subtarget) == false)
            {
                if(subtarget == PreDefineResources.inst.RuleManager)
                {
                    var res_Data = new { rules = new List<Rule_Engine>() };
                    foreach (var item in _ruleManager.RulID_RuleData_Pair)
                    {
                        res_Data.rules.Add(item.Value.LGAPI_Rule_Engine_Info);
                    }
                    SendMessage(context, Grapevine.Shared.HttpStatusCode.Ok, res_Data.rules.ToArray());
                    Console.WriteLine("Rule GET Success");
                }
                else if(_ruleManager.RulID_RuleData_Pair.ContainsKey(subtarget))
                {
                    SendMessage(context,Grapevine.Shared.HttpStatusCode.Ok,_ruleManager.RulID_RuleData_Pair[subtarget].LGAPI_Rule_Engine_Info);
                    Console.WriteLine("Rule GET Success");
                }
                else
                {
                    CommonFuntions.LGAPISendErrorMessage(context, Grapevine.Shared.HttpStatusCode.NotFound, $"Request Resource {subtarget} Not Found");
                    Console.WriteLine($"Rule GET Failed.. Request Resource {subtarget} Not Found");
                }
            }
            else
            {
                CommonFuntions.LGAPISendErrorMessage(context, Grapevine.Shared.HttpStatusCode.BadRequest, $"{path} is Invalid Path");
            }
        }

        ///룰 수정
        public override void ExcutePUT(IHttpContext context, string path)
        {
            var target = RemoveFirstSegment(path);
            if(_ruleManager.RulID_RuleData_Pair.ContainsKey(target))
            {
                var obj = JSONHelper.GetObjectFromJSONString<Rule_Engine>(context.Request.Payload);
                if (_ruleManager.EditRule(obj, target))
                {
                    SendMessage(context, Grapevine.Shared.HttpStatusCode.Ok, _ruleManager.RulID_RuleData_Pair[target].LGAPI_Rule_Engine_Info);
                    Console.WriteLine("Rule PUT Success");
                }
                else
                {
                    CommonFuntions.LGAPISendErrorMessage(context, Grapevine.Shared.HttpStatusCode.BadRequest, $"Resource {target} Edit Fail");
                    Console.WriteLine($"Rule PUT Fail... Resource {target} Edit Fail");
                }
            }
            else
            {
                CommonFuntions.LGAPISendErrorMessage(context, Grapevine.Shared.HttpStatusCode.NotFound, $"Request Resource {target} Not Found");
                Console.WriteLine($"Rule PUT Fail... Request Resource {target} Not Found");
            }
        }

        ///룰 추가
        public override void ExcutePOST(IHttpContext context, string path)
        {
            var target = RemoveFirstSegment(path);
            if(target == PreDefineResources.inst.RuleManager)
            {
                var obj = JSONHelper.GetObjectFromJSONString<Rule_Engine>(context.Request.Payload);
                string message = string.Empty;

                if (obj.brand != "NK")
                {
                    CommonFuntions.LGAPISendErrorMessage(context, Grapevine.Shared.HttpStatusCode.BadRequest, $"Resource {target} Create Fail Reason : brand not correct");
                    Console.WriteLine($"Rule POST Failed... Resource {target} Create Fail Reason : brand not correct");
                }

                if (_ruleManager.CreateRule(obj,ref message))
                {
                    // Path에 /두개 생기는 문제 해결하기 위해 하나 삭제
                    // CreateURL에서 / + "경로"로 처리하는 문제 때문에 발생
                    // 여기 외에는 정상 작동해서 여기만 예외 처리..
                    string TempBaseURL = String.Empty;
                    TempBaseURL = _BaseURL.Remove(0, 1);
                    _ruleManager.RulID_RuleData_Pair[message].LGAPI_Rule_Engine_Info._links._self.href = PreDefineResources.inst.CreateURL(TempBaseURL, message);
                    _ruleManager.RulID_RuleData_Pair[message].LGAPI_Rule_Engine_Info.brand = obj.brand;
                    SendMessage(context, Grapevine.Shared.HttpStatusCode.Created, _ruleManager.RulID_RuleData_Pair[message].LGAPI_Rule_Engine_Info);
                    Console.WriteLine("Rule POST Success");
                }
                else
                {
                    CommonFuntions.LGAPISendErrorMessage(context, Grapevine.Shared.HttpStatusCode.BadRequest, $"Resource {target} Create Fail Reason : {message}");
                    Console.WriteLine($"Rule POST Failed... Resource {target} Create Fail Reason : {message}");
                }
            }
            else
            {
                CommonFuntions.LGAPISendErrorMessage(context, Grapevine.Shared.HttpStatusCode.NotFound, $"Request Resource {target} Not Found");
                Console.WriteLine($"Rule POST Failed... Request Resource {target} Not Found");
            }
        }

        ///룰 삭제
        public override void ExcuteDELETE(IHttpContext context, string path)
        {
            var target = RemoveFirstSegment(path);
            if(_ruleManager.RulID_RuleData_Pair.ContainsKey(target))
            {
                if(_ruleManager.RemoveRule(target))
                {
                    SendMessage(context, Grapevine.Shared.HttpStatusCode.NoContent, "");
                    Console.WriteLine("Rule DELETE Success");
                }
                else
                {
                    CommonFuntions.LGAPISendErrorMessage(context, Grapevine.Shared.HttpStatusCode.NotFound, $"Resource {target} Remove Fail");
                    Console.WriteLine($"Rule DELETE Failed... Resource {target} Remove Fail");
                }
            }
            else
            {
                CommonFuntions.LGAPISendErrorMessage(context, Grapevine.Shared.HttpStatusCode.NotFound, $"Request Resource {target} Not Found");
                Console.WriteLine($"Rule DELETE Failed... Request Resource {target} Not Found");
            }
        }

        public List<LGAPI_Rule> ReportAllRule()
        {

            return _ruleManager.RuleReporter;
        }
    }
}
