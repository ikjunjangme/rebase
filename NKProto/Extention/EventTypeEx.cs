using PredefineConstant.Enum.Analysis.EventType;
using System;

namespace NKProto.Extention
{
    public static class EventTypeEx
    {
        public static IntegrationEventType ToEventType(this VAMetaService.EventType eventType)
        {
            switch (eventType)
            {
                //case VAMetaService.EventType.Longstay:
                //    return IntegrationEventType.Longstay;
                case VAMetaService.EventType.Loitering:
                    return IntegrationEventType.Loitering;
                case VAMetaService.EventType.Intrusion:
                    return IntegrationEventType.Intrusion;
                case VAMetaService.EventType.Falldown:
                    return IntegrationEventType.Falldown;
                case VAMetaService.EventType.LineCrossing:
                    return IntegrationEventType.LineCrossing;
                case VAMetaService.EventType.LineEnter:
                    return IntegrationEventType.LineEnter;
                case VAMetaService.EventType.Congestion:
                    return IntegrationEventType.Congestion;
                //case VAMetaService.EventType.AbnormalObjectCount:
                //    return IntegrationEventType.AbnormalObjectCount;
                //case VAMetaService.EventType.RoiCount:
                //    return IntegrationEventType.RoiCount;
                //case VAMetaService.EventType.LineCount:
                //    return IntegrationEventType.LineCount;
                case VAMetaService.EventType.IllegalParking:
                    return IntegrationEventType.IllegalParking;
                //case VAMetaService.EventType.WrongWay:
                //    return IntegrationEventType.WrongWay;
                case VAMetaService.EventType.DirectionCount:
                    return IntegrationEventType.Direction;
                //case VAMetaService.EventType.CongestionLevel:
                //    return IntegrationEventType.CongestionLevel;
                //case VAMetaService.EventType.VehicleSpeed:
                //    return IntegrationEventType.Speed;
                //case VAMetaService.EventType.StopVehicleCount:
                //    return IntegrationEventType.StopCounting;
                case VAMetaService.EventType.UnMaskedFace:
                    return IntegrationEventType.UnMaskedFace;
                case VAMetaService.EventType.MatchingFace:
                    return IntegrationEventType.MatchingFace;
                case VAMetaService.EventType.FireFlame:
                    return IntegrationEventType.Flame;
                case VAMetaService.EventType.FireSmoke:
                    return IntegrationEventType.Smoke;
                default:
                    return IntegrationEventType.Loitering;
                    throw new NotImplementedException();
            }
        }
    }
}
