using PredefineConstant.Enum.Analysis;
using System.Collections.Generic;
using System.Linq;

namespace PredefineConstant.Extenstion
{
    public static class ObjectTypeEx
    {
        public static List<ClassId> ToClassIds(this ObjectType ot)
        {
            List<ClassId> classIds = new();
            System.Enum.GetValues(typeof(ClassId))
                   .OfType<ClassId>()
                   .Where(x => x.ToString().ToLower().Contains(ot.ToString().ToLower()))
                   .ToList()
                   .ForEach(x => classIds.Add(x));

            return classIds;
        }
    }
}
