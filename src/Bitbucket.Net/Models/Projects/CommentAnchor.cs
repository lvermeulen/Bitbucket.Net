namespace Bitbucket.Net.Models.Projects
{
    public class CommentAnchor
    {
        public int? Line { get; set; }
        public LineTypes LineType { get; set; }
        public FileTypes FileType { get; set; }
        public string Path { get; set; }
        public string SrcPath { get; set; }
    }
}