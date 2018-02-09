namespace Bitbucket.Net.Models.Projects
{
    public class BuildStatusMetadata
    {
        public int Successful { get; set; }
        public int InProgress { get; set; }
        public int Failed { get; set; }
    }
}