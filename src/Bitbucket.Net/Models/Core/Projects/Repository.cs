namespace Bitbucket.Net.Models.Core.Projects
{
    public class Repository : RepositoryRef
    {
        public int Id { get; set; }
        public string ScmId { get; set; }
        public string State { get; set; }
        public string StatusMessage { get; set; }
        public bool Forkable { get; set; }
        public bool Public { get; set; }
        public CloneLinks Links { get; set; }

        public override string ToString() => Name;
    }
}
