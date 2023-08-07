using Grapevine.Interfaces.Server;
using Grapevine.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using PublicUtility.Event.Enum;
using LGAPIGateway.Models;
using LGAPIGateway.Singletons;

namespace LGAPIGateway
{
    public class CommonFuntions
    {
        public static void LGAPISendErrorMessage(IHttpContext Context, HttpStatusCode ErrorCode, string ErrorMessage)
        {
            var message = "{" + $"\"error-code\":{(int)ErrorCode},\"error-message\":\"{ErrorMessage}\"" + "}";
            Context.Response.StatusCode = ErrorCode;
            Context.Response.SendResponse(Encoding.UTF8.GetBytes(message));
        }

        public static string CreateUID()
        {
            var rand = new Random(Guid.NewGuid().GetHashCode());
            var buf = new byte[8];
            rand.NextBytes(buf);
            var id = string.Format("{0:X}", BitConverter.ToUInt64(buf));
            return id;
        }

        public static string LoadJson(string path)
        {
            if (File.Exists(path))
            {
                using (var fs = new StreamReader(path))
                {
                    return fs.ReadToEnd();
                }
            }
            else
            {
                return null;
            }
        }

        public static ObjectType GetNKObjectEnumFromeLGEnum(LG_Object_Type type)
        {
            var result = ObjectType.PERSON;
            if (Enum.TryParse<ObjectType>(type.ToString(), out result) == false)
            {
                result = ObjectType.NOT_DEFINE;
            }

            return result;
        }

        public static LG_Object_Type GetLGObjectEnumFromeNKEnum(ObjectType type)
        {
            var result = LG_Object_Type.NOTCLASSIFICATION;
            if (Enum.TryParse<LG_Object_Type>(type.ToString(), out result) == false)
            {
                result = LG_Object_Type.NOTCLASSIFICATION;
            }

            if(type == ObjectType.FLAME)
            {
                return LG_Object_Type.FLAME;
            }
            else if (type == ObjectType.SMOKE)
            {
                return LG_Object_Type.SMOKE;
            }
            else if (type == ObjectType.FACE_MAN)
            {
                return LG_Object_Type.FACEMAN;
            }
            else if (type == ObjectType.FACE_WOMAN)
            {
                return LG_Object_Type.FACEWOMAN;
            }
            else if (type == ObjectType.FACE_HEAD)
            {
                return LG_Object_Type.FACEMAN;
            }
            else if (type == ObjectType.FACE_HELMET)
            {
                return LG_Object_Type.FACEHELMET;
            }

            return result;
        }

        public static EventType GetNKEventFromLGEvent(LG_Event_Type type)
        {
            //var result = EventType.EVT_LOITERING;
            try
            {
                switch(type)
                {
                    case LG_Event_Type.Flame_Detect:
                        return EventType.EVT_FIRE_FLAME;
                    case LG_Event_Type.Steam_Smoke:
                        return EventType.EVT_FIRE_SMOKE;
                    case LG_Event_Type.LineCrossCar:
                    case LG_Event_Type.LineCrossCount:
                    case LG_Event_Type.LineCrossPersonCount:
                        return EventType.EVT_LINE_COUNT;
                    case LG_Event_Type.Loiter:
                        return EventType.EVT_LOITERING;
                    case LG_Event_Type.Intrusion:
                        return EventType.EVT_INTRUSION;
                    case LG_Event_Type.HelmetUnWear:
                        return EventType.EVT_HEAD_NO_HELMET;
                    case LG_Event_Type.HelmetWear:
                        return EventType.EVT_HEAD_HELMET;
                    case LG_Event_Type.FallDown:
                        return EventType.EVT_FALLDOWN;
                    case LG_Event_Type.Fight:
                        return EventType.EVT_VIOLENCE;
                    case LG_Event_Type.IlegalCar:
                        return EventType.EVT_ILLEGAL_PARKING;
                    case LG_Event_Type.Abnormal:
                        return EventType.EVT_LOITERING;
                    case LG_Event_Type.MaskUnWear:
                        return EventType.EVT_FACE_MASKED;
                    case LG_Event_Type.Abandon:
                         return EventType.EVT_ABANDONMENT;
                    default:
                        return EventType.EVT_LOITERING;
                }
                //result = (EventType)GlobalConfigs.inst.Event_Pair.Where(o => { return o.LGEventCode == (int)(type); }).First().NKEventCode;
            }
            catch (Exception ex) { }
            //return result;
            return EventType.EVT_LOITERING;
        }


        public static LG_Event_Type GetLGEventFromNKEvent(int type)
        {
            //var result = EventType.EVT_LOITERING;
            try
            {
                switch ((EventType)type)
                {
                    case EventType.EVT_FIRE_FLAME:
                        return LG_Event_Type.Flame_Detect;
                    case EventType.EVT_FIRE_SMOKE:
                        return LG_Event_Type.Steam_Smoke;
                    case EventType.EVT_LINE_COUNT:
                        return LG_Event_Type.LineCrossCount;
                    case EventType.EVT_LOITERING:
                        return LG_Event_Type.Loiter;
                    case EventType.EVT_INTRUSION:
                        return LG_Event_Type.Intrusion;
                    case EventType.EVT_HEAD_NO_HELMET:
                        return LG_Event_Type.HelmetUnWear;
                    case EventType.EVT_HEAD_HELMET:
                        return LG_Event_Type.HelmetWear;
                    case EventType.EVT_FALLDOWN:
                        return LG_Event_Type.FallDown;
                    case EventType.EVT_VIOLENCE:
                        return LG_Event_Type.Fight;
                    case EventType.EVT_ILLEGAL_PARKING:
                        return LG_Event_Type.IlegalCar;
                    case EventType.EVT_FACE_MASKED:
                        return LG_Event_Type.MaskUnWear;
                    case EventType.EVT_ABANDONMENT:
                        return LG_Event_Type.Abandon;
                    default:
                        return LG_Event_Type.Abnormal;
                }
                //result = (EventType)GlobalConfigs.inst.Event_Pair.Where(o => { return o.LGEventCode == (int)(type); }).First().NKEventCode;
            }
            catch (Exception ex) { }
            //return result;
            return LG_Event_Type.Loiter;
        }


        public static LG_Event_Type GetLGEventEnumFromeNKEnum(EventType type)
        {
            var result = LG_Event_Type.NotDefine;
            try
            {
                result = (LG_Event_Type)GlobalConfigs.inst.Event_Pair.Where(o => { return o.LGEventCode == (int)(type); }).First().NKEventCode;
            }
            catch (Exception ex) { }
            return result;
        }
    }
}
