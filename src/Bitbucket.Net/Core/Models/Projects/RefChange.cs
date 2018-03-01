namespace Bitbucket.Net.Core.Models.Projects
{
    public class RefChange
    {
        public Ref Ref { get; set; }
        public string RefId { get; set; }
        public string FromHash { get; set; }
        public string ToHash { get; set; }
        public string Type { get; set; }
    }
}