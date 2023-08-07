using PublicUtility.Event.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicUtility.Event
{
    public static class EventConverter
    {
        public static EventType GetEvent(int type)
        {
            return (EventType)type;
        }
        public static EventType GetEvent(string type)
        {
            return (EventType)System.Enum.Parse(typeof(EventType), type);
        }
    }
}
