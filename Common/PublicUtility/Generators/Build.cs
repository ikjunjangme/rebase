using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicUtility.Generators
{
    public class Build
    {
        static public DateTime GetAssemblyDateTime()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();

            return System.IO.File.GetLastWriteTime(assembly.Location);
        }
    }
}
