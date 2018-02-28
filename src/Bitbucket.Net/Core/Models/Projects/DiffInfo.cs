namespace Bitbucket.Net.Core.Models.Projects
{
    public abstract class DiffInfo
    {
        public string Truncated { get; set; }
        public string ContextLines { get; set; }
        public string FromHash { get; set; }
        public string ToHash { get; set; }
        public string WhiteSpace { get; set; }
    }
}