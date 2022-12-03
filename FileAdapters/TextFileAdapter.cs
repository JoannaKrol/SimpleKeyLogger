using System;
using System.IO;

namespace SimpleKeyLogger.FileAdapters
{
    public class TextFileAdapter 
    {
        private readonly string _fileName;
        private readonly string _filePath;
        private readonly string _path;

        public TextFileAdapter(string fileName, string filePath)
        {
            _fileName = fileName;
            _filePath = filePath;
            _path = Environment.CurrentDirectory + "\\" + _filePath + "\\" + _fileName + ".txt"; 
            CreateFile();
        }
        private void CreateFile()
        {

            if (!Directory.Exists(_filePath) && _filePath != "")
            {
                Directory.CreateDirectory(_filePath);
            }

            if (File.Exists(_path))
            {
                DelateFile();
            }

            using (StreamWriter sw = File.CreateText(_path)) { }
          
        }
        public void DelateFile()
        {
            try
            {
                File.Delete(_path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void WriteToFile(char sign)
        {
            using (StreamWriter sw = new StreamWriter(_path)) 
            {
                sw.Write(sign);
            }

        }
        public string ReadTextFile()
        {
            return File.ReadAllText(_path);
        }
    }
}
