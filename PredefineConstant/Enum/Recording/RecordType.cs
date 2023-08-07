using System;

namespace PredefineConstant.Enum.Recording
{
    [Flags]
    public enum RecordType
    {
        None = 0,
        Video = 1 << 0,
        Event = 1 << 1,
        VideoNEvent = ~(-1 << 2)
    }
}