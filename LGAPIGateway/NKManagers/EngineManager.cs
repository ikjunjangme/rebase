using LGAPIGateway;
using LGAPIGateway.Models;
using Newtonsoft.Json;
using NKAPI.API.Model;
using NKAPI.API.Repuest;
using NKAPI.API.Response;
using NKAPI.Proxy.RestApi;
using PublicUtility.API;
using RestAPIManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LGAPIGateway.NKManagers
{
    /// <summary>
    /// NK CN과 Channel을 LG에서 사용하는 분석 엔진으로 관리하는 클래스
    /// </summary>
    public class EngineManager : ManagerBase
    {
        public static Dictionary<string, Compute_Node_Info> _nodes;

        public static Dictionary<string, LGAPI_Analytic_Engine> _engines;

        public static Dictionary<string, NK_Analytic_Engine> _EngineID_Channel_Pair;

        public EngineManager()
        {
            _nodes = new Dictionary<string, Compute_Node_Info>();
            _engines = new Dictionary<string, LGAPI_Analytic_Engine>();
            _EngineID_Channel_Pair = new Dictionary<string, NK_Analytic_Engine>();
            _restClient = new RestClient(CreateNKBaseURL());
        }

        public static void WriteEngineFile()
        {
            try
            {
                var folder = new DirectoryInfo(_folderPath);
                if (folder.Exists == false)
                    folder.Create();

                var engine = new RestoreEngine
                {
                    Nodes = _nodes,
                    Engines = _engines,
                    EngineID_Channel_Pair = _EngineID_Channel_Pair
                };

                string jsonString = JsonConvert.SerializeObject(engine, Formatting.Indented, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                });
                File.WriteAllText(_enginefilePath, jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed save to backup");
            }
        }

        /// <summary>
        /// NK AG에게 노드 생성과 채널 생성을 송신하여 분석엔진을 생성한다.
        /// </summary>
        /// <returns>성공/실패</returns>
        public bool CreateEngine(LGAPI_Analytic_Engine engine, ref string resultORID, bool isRestore = false)
        {
            var result = false;
            if(GetAllChannelCount() >= PreDefineResources.inst.MaxEngineCount)
            {
                resultORID = "Engine Max Count";
                return result;
            }
            
            if(ValidRequestCheck(engine) == false)
            {
                result = false;
                resultORID = $"Invalid Request Null OR Empty Datas";
                return result;
            }

            var TargetNode = CreateNodeCheck();

            if(string.IsNullOrEmpty(TargetNode))
            {
                if(CNCreateRequest(ref TargetNode) == false)
                {
                    resultORID = TargetNode;
                    return result;
                }
            }

            var inst = CHCreateRequest(TargetNode, engine, ref resultORID);

            if (inst == null) //Error Check
            {
                return result;
            }
            else
            {
                //CH에 대한 정보가 적어서 정보 요청을 다시 하는 부분
                var ChannelGet = CreateRequestChannel(inst.NodeID, inst.ChannelID);
                var channel = JSONHelper.GetObjectFromJSONString< ResponseChannelInfoMsg>(_restClient.Excute(PreDefineResources.inst.RequestChannelInfo, ChannelGet, RestSharp.Method.Post).Result.Content);
                
                if (channel.code == (int)APIErrorCode.Ok)
                {
                    var engineinst = new NK_Analytic_Engine();

                    engineinst.Channel_Information = new ChannelInfo()
                    {
                        channelId = channel.channelId,
                        channelName = channel.channelName,
                        inputUri = channel.inputUri,
                        nodeId = channel.nodeId ??= inst.NodeID,
                        siblings = new List<string>(channel.siblings ??= new List<string>()),
                        status = channel.status,
                        FrameWidth = channel.FrameWidth,
                        FrameHeight = channel.FrameHeight
                    };

                    engineinst.Engine_Instance = inst;
                    engineinst.Channel_Information.channelId = ChannelGet.channelId;

                    _EngineID_Channel_Pair.Add(inst.Engine_ID, engineinst);

                    engine.engine_id = inst.Engine_ID;
                    engine._links = new LGAPI_AnalyticEngineLinks();
                    engine._links._self.href = PreDefineResources.inst.CreateURL(PreDefineResources.inst.BaseURL, PreDefineResources.inst.AnalyticEngineURL, inst.Engine_ID);
                    engine._links.rule_engines.href = PreDefineResources.inst.CreateURL(PreDefineResources.inst.BaseURL, PreDefineResources.inst.AnalyticEngineURL, inst.Engine_ID, PreDefineResources.inst.RuleManager);
                    _engines.Add(inst.Engine_ID, engine);
                    resultORID = inst.Engine_ID;

                    _nodes[TargetNode].Channels.Add(channel.channelId);
                    engineinst.RuleEngineResource.Init(engineinst.Engine_Instance, engine._links._self.href);

                    ChangeGRPC(engineinst.Channel_Information.channelId, engine, isRestore);
                    Console.WriteLine("Create Engine Success");
                    result = true;
                }

            }
            WriteEngineFile();
            return result;
        }

        public bool RemoveEngine(string id,ref string resultstring)
        {
            var result = false;
            
            string nodeid = _EngineID_Channel_Pair[id].Engine_Instance.NodeID;
            var req = CreateRequestChannel(nodeid, _EngineID_Channel_Pair[id].Engine_Instance.ChannelID);
            var str = _restClient.Excute(PreDefineResources.inst.RequestChannelDelete, req, RestSharp.Method.Post).Result.Content;
            var ack = JSONHelper.GetObjectFromJSONString<ResponseOnlyNode>(str);
            //ROI 삭제 안하고 엔진 삭제시 제대로 처리 되는지 확인 필요
            if (ack.code == 0)
            {
                var idx = _nodes[nodeid].Channels.IndexOf(_EngineID_Channel_Pair[id].Engine_Instance.ChannelID);
                if (idx != -1)
                {
                    _nodes[nodeid].Channels.RemoveAt(idx);
                }
                _EngineID_Channel_Pair.Remove(id);
                _engines.Remove(id);
                Console.WriteLine("Remove Engine Success");

                result = true;
            }
            else
            {
                resultstring = $"Remove Error Reason : {ack.message}";
                Console.WriteLine($"Remove Engine Error Reason : {ack.message}");
            }
            WriteEngineFile();
            return result;
        }

        public bool EditEngine(LGAPI_Analytic_Engine cmd, string engineid)
        {
            var result = false;

            var edited = _engines[engineid];
            ChangeGRPC(engineid, cmd);
            edited.fps = cmd.fps != 0 ? cmd.fps : edited.fps;
            edited.sensitivity = cmd.sensitivity != 0 ? cmd.sensitivity : edited.sensitivity;
            
            if(cmd.video != null)
            {
                edited.video.login_pwd = cmd.video.login_pwd;
                edited.video.login_id = cmd.video.login_id;
                edited.video.url = cmd.video.url;
                
                //Todo url 변경 정보 전송
            }

            if(cmd.meta != null)
            {
                edited.meta.filters = cmd.meta.filters ??= edited.meta.filters;
                edited.meta.login_id = cmd.meta.login_id ??= edited.meta.login_id;
                edited.meta.login_pwd = cmd.meta.login_pwd ??= edited.meta.login_pwd;
                edited.meta.url = cmd.meta.url ??= edited.meta.url;

                Console.WriteLine($"meta url : {edited.meta.url}");
            }

            if (cmd._links != null)
            {
                edited._links._self.href = cmd._links._self.href ??= edited._links._self.href;
                edited._links.rule_engines.href = cmd._links.rule_engines.href ??= edited._links.rule_engines.href;
            }

            //API 지원 안됨
            var req = CreateRequestChannel(engineid);
            req.channels = new List<ChannelInfo>();
            req.channels.Add(new ChannelInfo()
            {
                inputUri = edited.video.url,
                channelId = req.channelId,
                nodeId = req.nodeId
            });
            var ack = JSONHelper.GetObjectFromJSONString<ResponseOnlyNode>(_restClient.Excute(PreDefineResources.inst.RequestEditChannel, req, RestSharp.Method.Post).Result.Content);

            if(ack.code != 0)
            {
                Console.WriteLine("Edit Engine Failed");
                result = false;
            }
            else
            {
                Console.WriteLine("Edit Engine Success");
                result = true;
            }
            WriteEngineFile();
            return result;
        }

        public void ChangeGRPC(string target, LGAPI_Analytic_Engine cmd, bool isBACKUP = false)
        {
            var edited = _engines[target];
            var engineinst = _EngineID_Channel_Pair[target];
            //if (cmd.enable != edited.enable)
            {
                var VAResultString = string.Empty;

                if(isBACKUP == true)
                {
                    VAResultString = edited.grpcURL;
                    engineinst.MetaManager = new MetaManager(edited.meta);
                    engineinst.MetaManager.StartGRPC(target, VAResultString);
                    return;
                }

                if (cmd.enable)
                {
                    if (engineinst.MetaManager?.IsStartGRPC == false && VAControlSend(target, VAOperations.VA_START, ref VAResultString))
                    {
                        engineinst.MetaManager.StartGRPC(target, VAResultString);
                    }
                }
                else
                {
                    if (engineinst.MetaManager?.IsStartGRPC == true && VAControlSend(target, VAOperations.VA_STOP, ref VAResultString))
                    {
                        engineinst.MetaManager.StopGRPC();
                    }
                }
            }
        }

        public bool ValidRequestCheck(LGAPI_Analytic_Engine engine)
        {
            if(engine?.video != null && string.IsNullOrEmpty(engine.engine_name) && engine?.meta != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// VA Start 처리
        /// </summary>
        /// <returns>GRPC Host 주소 반환</returns>
        public bool VAControlSend(string target, VAOperations option, ref string ResultORError)
        {
            var result = false;

            var vacon = new RequestVAControl();
            vacon.channelIds = new List<string>();
            vacon.nodeid = GetNodeIDFromChannelID(target);
            vacon.operation = option;
            vacon.channelIds.Add(target);

            var response = JSONHelper.GetObjectFromJSONString<ResponseVacStartMsg>(_restClient.Excute(PreDefineResources.inst.RequestVAControl, vacon, RestSharp.Method.Post).Result.Content);
            
            if(response != null)
            {
                if (response.code == 0)
                {
                    ResultORError = $"{response.sourceIp}:{response.sourcePort}";
                    result = true;
                }
                else
                {
                    ResultORError = response.message;
                }
            }
            else
            {
                ResultORError = "NK Server NOT Responding..";
            }

            Console.WriteLine($"{target} : {ResultORError}");

            return result;
        }

        public int GetAllChannelCount()
        {
            int result = 0;
            foreach (var item in _nodes)
            {
                result += item.Value.Channels.Count();
            }
            return result;
        }

        private static readonly string _folderPath = AppDomain.CurrentDomain.BaseDirectory + "config";
        private static readonly string _enginefilePath = AppDomain.CurrentDomain.BaseDirectory + "config\\enginlist.json";
        public void ClearChannel()
        {
            var cnlist = GetAllNode();
            if (cnlist != null)
            {
                foreach (var node in cnlist)
                {
                    var req = CreateRequestChannel(node.nodeId, "");
                    var str = _restClient.Excute(PreDefineResources.inst.RequestGetAllChannel, req, RestSharp.Method.Post).Result.Content;
                    var list = JSONHelper.GetObjectFromJSONString<ResponseChannelListMsg>(str);
                    if (list.channels != null)
                    {
                        foreach (var item in list.channels)
                        {
                            var deletereq = CreateRequestChannel(node.nodeId, item.channelId);
                            var str2 = _restClient.Excute(PreDefineResources.inst.RequestChannelDelete, deletereq, RestSharp.Method.Post).Result.Content;
                        }
                    }

                    var delcomreq = new RequestCompute();
                    delcomreq.nodeId = node.nodeId;
                    var tmp = _restClient.Excute(PreDefineResources.inst.RequestNodeDelete, delcomreq, RestSharp.Method.Post).Result;
                }
            }
        }

        public string GetNodeIDFromChannelID(string channelID)
        {
            var result = string.Empty;
            foreach (var item in _nodes)
            {
                if(item.Value.Channels.Contains(channelID))
                {
                    result = item.Key;
                }
            }

            return result;
        }

        public string CreateNodeCheck()
        {
            //Node Info의 채널 카운트를 보고 노드 선택
            var result = string.Empty;

            foreach (var item in _nodes)
            {
                var node = (Compute_Node_Info)item.Value;
                if(node.Channels.Count < 8)
                {
                    result = item.Key;
                    break;
                }
            }

            return result;
        }

        private bool CNCreateRequest(ref string NodeORError)
        {
            bool result = false;
            var request = new RequestCompute();
            request.nodeId = request.nodeName = CommonFuntions.CreateUID();
            request.httpPort = 8880;
            request.host = PreDefineResources.inst.NKAPIBaseURL;

            //예외처리 필요 API 게이트웨이가 동작 안할때 보완 로직 필요
            string responseJSON = _restClient.Excute(PreDefineResources.inst.RequestNodeCreate, request, RestSharp.Method.Post).Result.Content;
            if (string.IsNullOrEmpty(responseJSON))
            {
                NodeORError = "Server Not Work";
                result = false;
            }
            else
            {
                var node = JSONHelper.GetObjectFromJSONString<ResponseOnlyNode>(responseJSON);
                if (node.code == 0)
                {
                    var nodeinfo = JSONHelper.GetObjectFromJSONString<ResponseCompute>(responseJSON);
                    var cn_info = new Compute_Node_Info(nodeinfo);
                    if (node.node.nodeId != null)
                    {
                        _nodes.Add(node.node.nodeId, cn_info);
                        NodeORError = node.node.nodeId;
                    }
                    result = true;
                }
                else
                {
                    NodeORError = $"Compute Node Create Fail \r\nReason : {node.message}";
                }
            }
            return result;
        }

        private Analytic_Engine_Instance CHCreateRequest(string TargetNode, LGAPI_Analytic_Engine engine, ref string ChannelORError)
        {
            Analytic_Engine_Instance result = null;
            Analytic_Engine_Instance inst = new Analytic_Engine_Instance();
            inst.NodeID = TargetNode;
            inst.ChannelID = inst.Engine_ID = CommonFuntions.CreateUID();
            
            var channelRequest = CreateRequestChannel(inst.NodeID, inst.ChannelID);
            channelRequest.inputType = InputType.SRC_IPCAM_NORMAL.ToString();
            channelRequest.channelName = inst.NodeID;
            channelRequest.channels = new List<ChannelInfo>();
            if(string.IsNullOrEmpty(engine.video.login_id) == false && string.IsNullOrEmpty(engine.video.login_pwd) == false)
            {
                var loginstr = $"{engine.video.login_id}:{engine.video.login_pwd}@";
                var rtspstring = engine.video.url.Insert("rtsp://".Length, loginstr);
                channelRequest.channels.Add(new ChannelInfo() { channelId = inst.ChannelID, nodeId = inst.NodeID, inputUri = rtspstring });
            }
            else
            {
                channelRequest.channels.Add(new ChannelInfo() { channelId = inst.ChannelID, nodeId = inst.NodeID, inputUri = $"{engine.video.url}" });
            }
            

            var str = _restClient.Excute(PreDefineResources.inst.RequestChannelCreate, channelRequest, RestSharp.Method.Post).Result.Content;

            if(string.IsNullOrEmpty(str))
            {
                ChannelORError = "Create Channel Request Server Time Out";
            }
            else
            {
                var ack = JSONHelper.GetObjectFromJSONString<ResponseChannelMsg>(str);
                if(ack.code != 0 && ack.code != 11)
                {
                    ChannelORError = ack.message + " : " + ((APIErrorCode)(ack.code)).ToString();
                }
                else
                {
                    result = inst;
                    ChannelORError = ack.channelId;
                }
            }
            return result;
        }

        private RequestChannel CreateRequestChannel(string nodeid,string channelid)
        {
            RequestChannel channel = new RequestChannel();
            channel.nodeId = nodeid;
            channel.channelId = channelid;
            return channel;
        }

        private RequestChannel CreateRequestChannel(string engineid)
        {
            RequestChannel channel = null;
            var nodeid = _EngineID_Channel_Pair[engineid].Engine_Instance.NodeID;
            if (string.IsNullOrEmpty(nodeid) == false)
            {
                channel = new RequestChannel();
                channel.nodeId = nodeid;
                channel.channelId = _EngineID_Channel_Pair[engineid].Engine_Instance.ChannelID;
            }
            return channel;
        }

        private List<ResponseComputingNode> GetAllNode()
        {
            var comreq = new RequestCompute();
            var str = _restClient.Excute(PreDefineResources.inst.RequestNodeList, comreq, RestSharp.Method.Post).Result.Content;
            var comlist = JSONHelper.GetObjectFromJSONString<ResponseComputingNodeListMsg>(str ??= "");

            return comlist?.nodes;
        }
    }
}
