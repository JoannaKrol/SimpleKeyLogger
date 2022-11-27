using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleKeyLogger.FileAdapters
{
    public class TextFileAdapter : FileAdapter
    {
        public TextFileAdapter(string fileName, string filePath): base(fileName, filePath, "txt")
        {
        }

        public override void WriteToFile(string text)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(text);
            fileStream.Write(bytes, 0, bytes.Length);
        }
    }
}
