namespace Bitbucket.Net.Models
{
    public class RepositoryRef
    {
        public string Slug { get; set; }
        public string Name { get; set; }
        public ProjectRef Project { get; set; }
    }
}
