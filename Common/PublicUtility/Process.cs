using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicUtility
{
    public class ProcessInfo
    {
        static public bool IsRun()
        {
            Process currentProcess = Process.GetCurrentProcess();
            Process[] procs = Process.GetProcessesByName(currentProcess.ProcessName);
            if (procs.Length > 1)
            {
                return true;
            }
            return false;
        }
    }
}
