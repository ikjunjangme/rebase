using PublicUtility.Event.Enum;
using System.Collections.Generic;

namespace PublicUtility.Event.Class
{
    public class EventFiltering
    {
        public int minDetectSize { get; set; }
        public int maxDetectSize { get; set; }
        public List<ObjectType> objectsTarget { get; set; }
    }
}
