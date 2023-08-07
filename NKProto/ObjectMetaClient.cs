using Grpc.Core;
using NKProto.Extention;
using PredefineConstant;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NKProto
{
    public class ObjectMetaClient : IMetaData, IAction
    {
        public string CameraUID { get; }
        public string CameraName { get; }

        private CancellationTokenSource _cts;
        private readonly Channel _channel;
        private readonly VAMetaService.VAMetaService.VAMetaServiceClient _client;
        private readonly Timer _timer;

        public event EventHandler<ObjectMeta> OnReceivedMetaData;

        /// <summary>
        /// Host : "ip:port"
        /// </summary>
        /// <param name="uID"></param>
        /// <param name="targetHost"></param>
        public ObjectMetaClient(string uID, string nickName, string targetHost)
        {
            CameraUID = uID;
            CameraName = nickName;
            _cts = new CancellationTokenSource();
            _channel = new Channel(targetHost, ChannelCredentials.Insecure);
            _client = new VAMetaService.VAMetaService.VAMetaServiceClient(_channel);

            _timer = new Timer(new TimerCallback(OnFpsTimer));
        }

        private int _fps;
        public int Fps { get; private set; }
        private void OnFpsTimer(object state)
        {
            Fps = _fps;

            _fps = 0;
        }

        public void StartTask()
        {
            _timer.Change(1000, 1000);

            Task.Run(async () =>
            {
                int delay = 1000;
                int cntError = 1;
                while (!_cts.IsCancellationRequested)
                {
                    try
                    {
                        var streamingCall = _client.GetVAMetaStream(new Google.Protobuf.WellKnownTypes.Empty(), cancellationToken: _cts.Token);
                        while (await streamingCall.ResponseStream.MoveNext(_cts.Token))
                        {
                            var meta = streamingCall.ResponseStream.Current;

                            if (CameraUID.Contains(meta.ChannelId))
                            {
                                //Debug.WriteLine($"{string.Join(",", meta.EventList.Select(x => x.AlramTrace.FirstOrDefault()?.AlramRoi.RoiId))}");

                                ObjectMeta objMeta = new ObjectMeta()
                                {
                                    CameraUID = CameraUID,
                                    CameraName = CameraName,
                                    EventList = new List<EventInfo>(),
                                    TimeStamp = meta.Timestamp == null ? DateTime.Now : meta.Timestamp.ToDateTime(),
                                    FrameWidth = meta.ImageWidth,
                                    FrameHeight = meta.ImageHeight,
                                };

                                try
                                {
                                    meta.ObjectList.ToList().ForEach(trkObj => objMeta.EventList.Add(new EventInfo()
                                    {
                                        IsEvent = false,
                                        ClassID = trkObj.Label.ToCalssId(),
                                        AbnormalScore = trkObj.Confidence,
                                        ImageRect = new RectangleF((float)trkObj.Box.X, (float)trkObj.Box.Y, (float)trkObj.Box.Width, (float)trkObj.Box.Height),
                                    }));
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine();
                                }

                                try
                                {
                                    meta.EventList.ToList().ForEach(evtObj =>
                                    {
                                        byte[] image = null;
                                        if (!string.IsNullOrEmpty(evtObj.JpegImage.Base64Image))
                                            image = Convert.FromBase64String(evtObj.JpegImage.Base64Image);

                                        objMeta.EventList.Add(new EventInfo()
                                        {
                                            IsEvent = true,
                                            ClassID = evtObj.Segmentation.Label.ToCalssId(),
                                            AbnormalScore = evtObj.Segmentation.Confidence,
                                            EventType = evtObj.EventType.ToEventType(),
                                            EventStatus = evtObj.State.ToEventStatus(),
                                            ObjectID = evtObj.Id,
                                            ImageBuffer = image,
                                            ImageRect = new RectangleF((float)evtObj.Segmentation.Box.X, (float)evtObj.Segmentation.Box.Y, (float)evtObj.Segmentation.Box.Width, (float)evtObj.Segmentation.Box.Height),
                                            InnerImageRect = new RectangleF((float)evtObj.JpegImage.ObjectBox.X, (float)evtObj.JpegImage.ObjectBox.Y, (float)evtObj.JpegImage.ObjectBox.Width, (float)evtObj.JpegImage.ObjectBox.Height),
                                            //EventType = 
                                            //필요없음
                                            RoiUId = evtObj.AlramTrace.FirstOrDefault()?.AlramRoi.RoiId,
                                            EventDetail = new FaceEventDetail() { EventScore = evtObj.EventScore, FaceUID = evtObj.EventMessage }
                                        });
                                    });
                                }
                                catch (Exception ee)
                                {
                                    Console.WriteLine();
                                }


                                //if (eventEvents.Any())
                                {
                                    try
                                    {
                                        OnReceivedMetaData?.Invoke(this, objMeta);
                                        _fps++;
                                    }
                                    catch (Exception ee)
                                    {
                                        Debug.WriteLine(ee);
                                    }
                                }
                            }

                        }
                    }
                    catch (Exception ee)
                    {
                        //Debug.WriteLine(ee.Message);
                        //delay = delay * cntError++;
                        //if (cntError > 10)
                        //{
                        //    delay = 1000;
                        //    cntError = 0;
                        //}
                    }
                    finally
                    {
                        await Task.Delay(delay);
                    }
                }
                await _channel.ShutdownAsync();
            });
        }

        public void StopTask()
        {
            _timer.Dispose();
            _cts.Cancel();
        }
    }
}

