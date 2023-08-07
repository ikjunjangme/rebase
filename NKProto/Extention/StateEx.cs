using PredefineConstant.Enum.Analysis;
using System;
using VAMetaService;

namespace NKProto.Extention
{
    public static class StateEx
    {
        public static Progress ToEventStatus(this State status)
        {
            switch (status)
            {
                case State.Start:
                    return Progress.Begin;
                case State.Continue:
                    return Progress.Continue;
                case State.End:
                    return Progress.End;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
