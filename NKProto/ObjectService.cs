using Grpc.Core;

namespace NKProto
{
    public class ObjectService : VAMetaService.VAMetaService.VAMetaServiceBase
    {
        private Server _service;

        //public override async Task GetObjectMetaStream(EmptyMsg request, IServerStreamWriter<ObjectMetaData> responseStream, ServerCallContext context)
        //{
        //    while (!context.CancellationToken.IsCancellationRequested)
        //    {
        //        var meta = new ObjectMetaData() { Timestamp = _n++ };
        //        await responseStream.WriteAsync(meta);
        //        await Task.Delay(30);
        //    }
        //}

        //ulong _n = 0;
        //public override async Task GetObjectMetaStream(EmptyMsg request, IServerStreamWriter<ObjectMetaData> responseStream, ServerCallContext context)
        //{
        //    while (!context.CancellationToken.IsCancellationRequested)
        //    {
        //        var meta = new ObjectMetaData() { Timestamp = _n++ };
        //        await responseStream.WriteAsync(meta);

        //        await Task.Delay(30);
        //    }
        //}

        ulong _n = 0;
        //public override async Task GetObjectMetaStream(Empty request, IServerStreamWriter<ObjectMetaData> responseStream, ServerCallContext context)
        //{
        //    while (!context.CancellationToken.IsCancellationRequested)
        //    {
        //        var meta = new ObjectMetaData() { Timestamp = _n++ };
        //        await responseStream.WriteAsync(meta);

        //        await Task.Delay(30);
        //    }
        //}

        public void StartWorker(string ip, int port)
        {
            _service = new Server()
            {
                Services = { VAMetaService.VAMetaService.BindService(this) },
                Ports = { new ServerPort(ip, port, ServerCredentials.Insecure) }
            };
            _service.Start();
        }
    }
}
