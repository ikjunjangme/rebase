using PredefineConstant;
using System;

namespace NKProto
{
    public interface IMetaData
    {
        public string CameraUID { get; }
        public string CameraName { get; }
        event EventHandler<ObjectMeta> OnReceivedMetaData;
        int Fps { get; }

    }
    public interface IAction
    {
        void StartTask();
        void StopTask();
    }
}
