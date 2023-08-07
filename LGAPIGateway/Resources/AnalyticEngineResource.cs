using Grapevine.Interfaces.Server;
using LGAPIGateway.Models;
using LGAPIGateway.NKManagers;
using Newtonsoft.Json;
using RestAPIManager;
using RestAPIService.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace LGAPIGateway.Resources
{
    public class AnalyticEngineResource : ResourceBase
    {
        /// <summary>
        /// 엔진 생성,삭제 등을 관리하는 엔진 매니저
        /// </summary>
        private EngineManager _engineManager;
        /// <summary>
        /// System Resource에게 엔진 전체 개수를 전달 하기 위한 델리게이트
        /// </summary>
        /// <param name="count">엔진 매니저가 가지고 있는 엔지의 개수</param>
        public delegate void UpdateEngine(int count);
        /// <summary>
        /// 엔진 개수를 전달 받기 위한 이벤트
        /// </summary>
        public event UpdateEngine OnUpdateEngine;

        private readonly string _enginefilePath = AppDomain.CurrentDomain.BaseDirectory + "config\\enginlist.json";
        private readonly string _folderPath = AppDomain.CurrentDomain.BaseDirectory + "config";

        public AnalyticEngineResource()
        {
            SubResources = new string[] { "engines" };
            _engineManager = new EngineManager();
        }

        /// 엔진 또는 룰 생성
        public override void ExcutePOST(IHttpContext context, string path)
        {
            var target = GetNTargetResource(path, 0);
            if (target == PreDefineResources.inst.EngineManager) // 엔진 생성일때
            {
                var obj = JSONHelper.GetObjectFromJSONString<LGAPI_Analytic_Engine>(context.Request.Payload);
                var idORError = string.Empty;
                var result = _engineManager.CreateEngine(obj,ref idORError);

                if (result)
                {
                    SendMessage(context, Grapevine.Shared.HttpStatusCode.Created, EngineManager._engines[idORError]);

                    OnUpdateEngine?.Invoke(EngineManager._engines.Count);

                    Console.WriteLine("Engine POST Success");
                }
                else
                {
                    CommonFuntions.LGAPISendErrorMessage(context, Grapevine.Shared.HttpStatusCode.BadRequest, idORError);
                    Console.WriteLine("Engine POST Fail");
                }
            }
            else if(EngineManager._engines.ContainsKey(target)) // 룰 생성일때
            {
                EngineManager._EngineID_Channel_Pair[target].RuleEngineResource.ExcutePOST(context, RemoveFirstSegment(path));
                if (EngineManager._EngineID_Channel_Pair[target].MetaManager == null)
                {
                    //Todo 메모리 릭 해결 필요
                    //이벤트가 등록 돼있으면 GC대상이 되지 않음
                    var metamgr = new MetaManager(EngineManager._engines[target].meta);
                    metamgr.OnStopGRPC += new System.EventHandler((o, e) =>
                    {
                        string tmp = string.Empty;
                        _engineManager.VAControlSend(target, NKAPI.API.Model.VAOperations.VA_STOP, ref tmp);
                    });
                    metamgr.ReportRuleEngine = new Func<List<LGAPI_Rule>>(EngineManager._EngineID_Channel_Pair[target].RuleEngineResource.ReportAllRule);
                    EngineManager._EngineID_Channel_Pair[target].MetaManager = metamgr;
                }
                if (EngineManager._EngineID_Channel_Pair[target].MetaManager.IsStartGRPC == false)
                {
                    var str = string.Empty;
                    if(EngineManager._engines[target].enable && _engineManager.VAControlSend(target, NKAPI.API.Model.VAOperations.VA_START, ref str))
                    {
                        EngineManager._EngineID_Channel_Pair[target].MetaManager.StartGRPC(target, str);

                        EngineManager._EngineID_Channel_Pair[target].GRPCAddr = str;
                        EngineManager._engines[target].grpcURL = str;
                    }
                }
            }
            else
            {
                CommonFuntions.LGAPISendErrorMessage(context, Grapevine.Shared.HttpStatusCode.NotFound, "");
            }
        }

        /// 엔진 또는 룰 수정
        public override void ExcutePUT(IHttpContext context, string path)
        {
            var target = GetNTargetResource(path, 0);
            if (EngineManager._EngineID_Channel_Pair.ContainsKey(target))
            {
                if (CheckNextPath(path)) //룰 엔진 수정일때
                {
                    EngineManager._EngineID_Channel_Pair[target].RuleEngineResource.ExcutePUT(context,RemoveFirstSegment(path));
                    Console.WriteLine("Engine/Rule PUT Complete");
                }
                else // 엔진 수정일때
                {
                    var obj = JSONHelper.GetObjectFromJSONString<LGAPI_Analytic_Engine>(context.Request.Payload);
                    if (_engineManager.EditEngine(obj, target))
                    {
                        SendMessage(context, Grapevine.Shared.HttpStatusCode.Ok, EngineManager._engines[target]);
                        Console.WriteLine("Engine PUT Complete");
                    }
                    else
                    {
                        CommonFuntions.LGAPISendErrorMessage(context, Grapevine.Shared.HttpStatusCode.BadRequest, "");
                        Console.WriteLine("Engine PUT Fail");
                    }
                }
            }
            else
            {
                CommonFuntions.LGAPISendErrorMessage(context, Grapevine.Shared.HttpStatusCode.NotFound, "");
            }
        }

        /// 엔진 또는 룰 삭제
        public override void ExcuteDELETE(IHttpContext context, string path)
        {
            var target = GetNTargetResource(path, 0);
            if (EngineManager._EngineID_Channel_Pair.ContainsKey(target))
            {
                if(CheckNextPath(path)) // 룰 엔진에게 삭제 전송
                {
                    EngineManager._EngineID_Channel_Pair[target].RuleEngineResource.ExcuteDELETE(context, RemoveFirstSegment(path));
                }
                else
                {
                    var resultstr = string.Empty;
                    if (_engineManager.RemoveEngine(path, ref resultstr))
                    {
                        SendMessage(context, Grapevine.Shared.HttpStatusCode.NoContent, "");
                        OnUpdateEngine(EngineManager._engines.Count);
                    }
                    else
                    {
                        CommonFuntions.LGAPISendErrorMessage(context, Grapevine.Shared.HttpStatusCode.BadRequest, resultstr);
                    }
                    OnUpdateEngine?.Invoke(EngineManager._engines.Count);
                }
                Console.WriteLine("Engine Delete Success");
            }
            else
            {
                CommonFuntions.LGAPISendErrorMessage(context, Grapevine.Shared.HttpStatusCode.NotFound, "");
                Console.WriteLine("Engine Delete Fail");
            }
        }

        /// 엔진 또는 룰 조회
        public override void ExcuteGET(IHttpContext context, string path)
        {
            var target = GetNTargetResource(path, 0);
            if (target == PreDefineResources.inst.EngineManager)
            {
                var arryas = new
                {
                    engines = new List<LGAPI_Analytic_Engine>(),
                };

                foreach (var item in EngineManager._engines)
                {
                    arryas.engines.Add(item.Value);
                }

                context.Response.SendResponse(JSONHelper.GetUTF8ByteArrayFromeObject(arryas.engines.ToArray()));
            }
            else if (EngineManager._EngineID_Channel_Pair.ContainsKey(target))
            {
                if (CheckNextPath(path))
                {
                    EngineManager._EngineID_Channel_Pair[target].RuleEngineResource.ExcuteGET(context,RemoveFirstSegment(path));
                }
                else
                {
                    context.Response.SendResponse(JSONHelper.GetUTF8ByteArrayFromeObject(EngineManager._engines[path]));
                }
            }
            else
            {
                CommonFuntions.LGAPISendErrorMessage(context, Grapevine.Shared.HttpStatusCode.BadRequest, "");
            }
        }

        public void DebugReset()
        {
            _engineManager.ClearChannel();
        }

        public void Restore()
        {
            try
            {
                var restore = LoadLGAPIEngineFile();
                if (restore == null) return;

                RuleManager _ruleManager = null;
                EngineManager._engines = restore.Engines;
                EngineManager._nodes = restore.Nodes;
                EngineManager._EngineID_Channel_Pair = restore.EngineID_Channel_Pair;

                foreach (KeyValuePair<string, LGAPI_Analytic_Engine> item in EngineManager._engines)
                {
                    var tmpRuleReporter = restore.EngineID_Channel_Pair[item.Key].RuleEngineResource._ruleManager.RuleReporter;
                    var tmpRulID_RuleData_Pair = restore.EngineID_Channel_Pair[item.Key].RuleEngineResource._ruleManager._RulID_RuleData_Pair;
                    var tmpChannelID = restore.EngineID_Channel_Pair[item.Key].RuleEngineResource._ruleManager._channelID;
                    var tmpNodeID = restore.EngineID_Channel_Pair[item.Key].RuleEngineResource._ruleManager._nodeID;

                    _ruleManager = EngineManager._EngineID_Channel_Pair[item.Key].RuleEngineResource.Init(EngineManager._EngineID_Channel_Pair[item.Key].Engine_Instance, item.Value._links._self.href);
                    EngineManager._EngineID_Channel_Pair[item.Key].MetaManager = new MetaManager(item.Value.meta);
                    EngineManager._EngineID_Channel_Pair[item.Key].MetaManager.ReportRuleEngine = new Func<List<LGAPI_Rule>>(EngineManager._EngineID_Channel_Pair[item.Key].RuleEngineResource.ReportAllRule);

                    if (item.Value.grpcURL != null)
                    {
                        EngineManager._EngineID_Channel_Pair[item.Key].MetaManager.StartGRPC(item.Key, item.Value.grpcURL);
                    }
                    _ruleManager.RuleReporter = tmpRuleReporter;
                    _ruleManager._RulID_RuleData_Pair = tmpRulID_RuleData_Pair;
                    _ruleManager._channelID = tmpChannelID;
                    _ruleManager._nodeID = tmpNodeID;
                }
                EngineManager.WriteEngineFile();
                Console.WriteLine("Restored Data");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Restoring Data Failed: {ex}");
            }
        }

        public RestoreEngine LoadLGAPIEngineFile()
        {
            try
            {
                var folder = new DirectoryInfo(_folderPath);
                if (folder.Exists == true)
                    if (File.Exists(_enginefilePath))
                    {
                        var jsonSerializerSettings = new JsonSerializerSettings();
                        jsonSerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;

                        string json = File.ReadAllText(_enginefilePath);
                        var lg_system = JsonConvert.DeserializeObject<RestoreEngine>(json, jsonSerializerSettings);

                        return lg_system;

                    }
            }
            catch
            {
                Console.WriteLine("Failed load to backup");
            }
            return null;
        }
    }
}
