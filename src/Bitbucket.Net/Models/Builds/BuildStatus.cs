namespace Bitbucket.Net.Models.Builds
{
    public class BuildStatus : KeyedUrl
    {
        public string State { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long DateAdded { get; set; }
    }
}
