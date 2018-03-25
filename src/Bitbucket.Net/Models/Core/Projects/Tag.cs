namespace Bitbucket.Net.Models.Projects
{
    public class Tag
    {
        public string Id { get; set; }
        public string DisplayId { get; set; }
        public string Type { get; set; }
        public string LatestCommit { get; set; }
        public string LatestChangeset { get; set; }
        public string Hash { get; set; }
    }
}
