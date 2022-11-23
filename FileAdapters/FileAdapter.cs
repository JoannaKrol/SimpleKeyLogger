using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleKeyLogger.FileAdapters
{
    public abstract class FileAdapter
    {
        public readonly string fileName;
        public readonly string filePath;
        public readonly string fileExtention;
        public string path;
        public FileStream fileStream;

        public FileAdapter(string fileName, string filePath, string fileExtention) 
        {
            this.fileName = fileName;
            this.filePath = filePath;
            this.fileExtention = fileExtention;
            path = Environment.CurrentDirectory + "\\" + filePath + "\\" + fileName + "." + fileExtention;
            CreateFile();
            OpenFile();
        }
        public void CreateFile() 
        {

            if (!Directory.Exists(filePath) && filePath != "")
            {
                Directory.CreateDirectory(filePath);
            }

            if (!File.Exists(path))
            {
                File.Create(path);
            }
        }
        public void DelateFile() 
        {
            try
            {
                File.Delete(path);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void OpenFile() 
        {
            fileStream = File.OpenWrite(path);
            
        }
        public void CloseFile() 
        {
            fileStream.Close();
        }
        public virtual void WriteToFile(string text) { }

    }
}
