namespace Bitbucket.Net.Models.Core.Admin
{
    public class MailServerConfiguration
    {
        public string HostName { get; set; }
        public int Port { get; set; }
        public string Protocol { get; set; }
        public bool UseStartTls { get; set; }
        public bool RequireStartTls { get; set; }
        public string UserName { get; set; }
        public string SenderAddress { get; set; }
    }

}
