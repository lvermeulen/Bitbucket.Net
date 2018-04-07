namespace Bitbucket.Net.Models.Ssh
{
    public class SshSettings
    {
        public Accesskeys AccessKeys { get; set; }
        public string BaseUrl { get; set; }
        public bool Enabled { get; set; }
        public Fingerprint Fingerprint { get; set; }
        public int Port { get; set; }
    }
}
