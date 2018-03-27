namespace Bitbucket.Net.Models.Core.Projects
{
    public class Change
    {
        public string ContentId { get; set; }
        public string FromContentId { get; set; }
        public Path Path { get; set; }
        public bool Executable { get; set; }
        public int PercentUnchanged { get; set; }
        public string Type { get; set; }
        public string NodeType { get; set; }
        public Path SrcPath { get; set; }
        public bool SrcExecutable { get; set; }
        public Links Links { get; set; }
    }
}
