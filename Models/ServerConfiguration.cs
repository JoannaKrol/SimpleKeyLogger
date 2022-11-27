namespace SimpleKeyLogger.Models
{
    public class ServerConfiguration
    {
        public string HostSmtp { get; set; }
        public bool EnableSsl { get; set; }
        public int Port { get; set; }
    }
}
