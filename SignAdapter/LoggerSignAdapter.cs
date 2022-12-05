using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleKeyLogger.SignAdapter
{
    public class LoggerSignAdapter
    {
        public readonly List<int> charSet;

        public LoggerSignAdapter()
        {
            charSet = new List<int>();
            for (int i = 48; i <= 57; i++) charSet.Add(i);
            for (int i = 65; i <= 90; i++) charSet.Add(i);
            for (int i = 96; i <= 105; i++) charSet.Add(i);
            charSet.Add(32);
            charSet.Add(13);
            charSet.Add(27);
        }
        public char GetSign (int signInt)
        {
            
            if (signInt >= 96 && signInt <= 105)
            {
                return (char)(signInt - 48);
            }
            else
            {
               return (char)signInt;
            }
        }
    }
}
