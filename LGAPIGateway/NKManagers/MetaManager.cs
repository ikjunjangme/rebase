using LGAPIGateway.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using VAMetaService;
using static VAMetaService.VAMetaService;

namespace LGAPIGateway.NKManagers
{
    public class MetaManager : ManagerBase
    {
        private Task _GRPCTask;
        private CancellationToken _stopToken;
        private VAMetaServiceClient _Client;
        private Grpc.Core.Channel _Channel;
        private string _Host;
        public event EventHandler OnStopGRPC;
        private LGAPI_Meta_Link _ConfigData;

        [JsonIgnore]
        [IgnoreDataMember]
        public Func<List<LGAPI_Rule>> ReportRuleEngine;
        
        public bool IsStartGRPC { get; private set; }

        public MetaManager(LGAPI_Meta_Link Meta)
        {
            _stopToken = new CancellationToken();
            if (Meta != null)
            {
                _restClient = new RestAPIManager.RestClient(Meta.url);
            }
            _ConfigData = Meta;
            IsStartGRPC = false;

            if(Meta != null)
            {
                if (string.IsNullOrEmpty(Meta.login_id) == false && string.IsNullOrEmpty(Meta.login_pwd) == false)
                {
                    _restClient.EnableAuth(Meta.login_id, Meta.login_pwd, RestAPIManager.AuthorizationHelper.AuthType.Basic);
                }
            }
        }

        public void StartGRPC(string channelId, string host = null)
        {
            IsStartGRPC = true;
            _Host = host ??= _Host;
            _Channel = new Grpc.Core.Channel(_Host, Grpc.Core.ChannelCredentials.Insecure);
            _Client = new VAMetaServiceClient(_Channel);

            EngineManager._engines.GetValueOrDefault(channelId).grpcURL = _Host;
            EngineManager.WriteEngineFile();
            Console.WriteLine($"Starting GRPC... ChannelId={channelId}, Host={_Host}");

            _GRPCTask = Task.Run(async () =>
            {
                while (!_stopToken.IsCancellationRequested)
                {
                    try
                    {
                        var stream = _Client.GetVAMetaStream(new Google.Protobuf.WellKnownTypes.Empty(), cancellationToken: _stopToken);
                        while (await stream.ResponseStream.MoveNext(_stopToken))
                        {
                            var meta = stream.ResponseStream.Current;
                            SendMetaDataToLG(meta);
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                    await Task.Delay(15);
                }
                OnStopGRPC?.Invoke(this, null);
                IsStartGRPC = false;
            });
        }

        public async void StopGRPC()
        {
            _stopToken.ThrowIfCancellationRequested();
            await _GRPCTask;
            _GRPCTask?.Dispose();
            _GRPCTask = null;
            await _Channel?.ShutdownAsync();
        }

        private void SendMetaDataToLG(FrameMetaData Data)
        {
            var meta = new LGAPI_Meta();
            meta.engine_id = Data.ChannelId;
            //meta.utc_time = Data.Timestamp.ToString();
            meta.utc_time = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fff");

            if (Data?.EventList.Count() > 0)
            {
                Console.WriteLine($"[{meta.utc_time}/{Data.ChannelId}]EventStamp ({Data?.EventList.Count()})");
            }

            if (_ConfigData.filters.Contains(1))
            {
                var frame = new LGAPI_Frame();
                meta.frame = frame;
                frame.rules = new List<LGAPI_Rule>(ReportRuleEngine?.Invoke());

                frame.objects = new List<LGAPI_Object>();
                foreach (var NKEvent in Data.EventList)
                {
                    var LGObject = new LGAPI_Object();
                    LGObject.id = NKEvent.Id;
                    LGObject.type = (int)CommonFuntions.GetLGObjectEnumFromeNKEnum((PublicUtility.Event.Enum.ObjectType)NKEvent.Segmentation.Label);
                    LGObject.box = new List<float>();
                    LGObject.box.Add(Convert.ToSingle(NKEvent.Segmentation.Box.Y));
                    LGObject.box.Add(Convert.ToSingle(NKEvent.Segmentation.Box.X));
                    LGObject.box.Add(Convert.ToSingle(NKEvent.Segmentation.Box.Y + NKEvent.Segmentation.Box.Height));
                    LGObject.box.Add(Convert.ToSingle(NKEvent.Segmentation.Box.X + NKEvent.Segmentation.Box.Width));
                    LGObject.evts = new List<string>();
                    LGObject.evts.Add(NKEvent.AlramTrace.Last().AlramRoi.RoiId);

                    //if (frame.rules.Any(x => x.type == LGObject.type) == false) return;

                    frame.objects.Add(LGObject);
                }
            }

            if (_ConfigData.filters.Contains(2))
            {
                meta.alarms = new List<LGAPI_Alarm>();

                var rules = new List<LGAPI_Rule>(ReportRuleEngine?.Invoke());

                foreach (var NKEvent in Data.EventList)
                {
                    var LGAlarm = new LGAPI_Alarm();
                    LGAlarm.id = NKEvent.AlramTrace.Last().AlramRoi.RoiId;
                    LGAlarm.type = (int)CommonFuntions.GetLGEventFromNKEvent((int)NKEvent.EventType);// (int)CommonFuntions.GetLGObjectEnumFromeNKEnum((PublicUtility.Event.Enum.ObjectType)NKEvent.Segmentation.Label);
                    LGAlarm.img = NKEvent.JpegImage.Base64Image;

                    if (rules.Any(x => x.type == LGAlarm.type) == false) return;

                    //Todo 매핑 필요?
                    //LGAlaram.level
                    //LGAlaram.count
                    meta.alarms.Add(LGAlarm);
                }
            }

            _restClient.Excute("", meta, RestSharp.Method.Put, 100);
        }

    }
}
