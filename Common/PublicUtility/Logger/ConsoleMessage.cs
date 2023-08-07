using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicUtility.Logger
{
    public static class ConsoleMessage
    {
        const string _back = "\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b";
        public static void WriteProgressBar(int percent, bool update = false)
        {
            if (update)
                Console.Write(_back);
            var p = (int)((percent / 10f) + .5f);
            for (var i = 0; i < 10; ++i)
            {
                if (i >= p)
                    Console.Write("");
                else
                    Console.Write("■");
            }
            Console.Write("...{0,3:##0}%", percent);
            if (percent == 100)
                Console.WriteLine();
        }
        public static void ConsoleProgressBar(int sol, int ust, int deger, int isaret)
        {
            int maxBarSize = Console.BufferWidth - 1;
            int barSize = deger;
            decimal f = 1;
            if (barSize + sol > maxBarSize)
            {
                barSize = maxBarSize - (sol + 5); // first 5 character "%100 "
                f = (decimal)deger / (decimal)barSize;
            }

            Console.CursorVisible = false;
            Console.SetCursorPosition(sol + 5, ust);
            for (int i = 0; i < barSize + 1; i++)
            {
                System.Threading.Thread.Sleep(10);
                Console.Write('\u25A0');
                Console.SetCursorPosition(sol, ust);
                Console.Write("%" + (i * f).ToString("0,0"));
                Console.SetCursorPosition(sol + 5 + i, ust);
            }
        }
    }
}
