namespace Bitbucket.Net.Models.Projects
{
    public class Commit
    {
        public string Id { get; set; }
        public string DisplayId { get; set; }
        public Author Author { get; set; }
        public long AuthorTimestamp { get; set; }
        public Author Committer { get; set; }
        public long CommitterTimestamp { get; set; }
        public string Message { get; set; }
        public CommitParent[] Parents { get; set; }
    }
}
