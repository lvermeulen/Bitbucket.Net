namespace Bitbucket.Net.Models.Core.Projects
{
    public class RepositoryOrigin
    {
        public string Slug { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string ScmId { get; set; }
        public string State { get; set; }
        public string StatusMessage { get; set; }
        public bool Forkable { get; set; }
        public Project Project { get; set; }
        public bool Public { get; set; }
        public Links Links { get; set; }
    }
}