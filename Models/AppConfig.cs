using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleKeyLogger.Models
{
    public class AppConfig
    {
        public int Limit { get; set; }
        public ServerConfiguration ServerConfiguration { set; get; }
        public SenderConfiguration SenderConfiguration { set; get; }
        public string RecipientEmail { set; get; }
    }
}
