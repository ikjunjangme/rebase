using System;
using System.Runtime.InteropServices;

namespace PublicUtility
{
    public static class CopyMemory
    {
        [DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory", SetLastError = false)]
        public static extern void PtrToPtr(IntPtr dst, IntPtr src, uint count);
    }
}
