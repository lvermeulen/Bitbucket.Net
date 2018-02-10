namespace Bitbucket.Net.Models.Projects
{
    public class CommentInfo
    {
        public string Text { get; set; }
        public CommentId Parent { get; set; }
        public CommentAnchor Anchor { get; set; }
    }
}
