using Bitbucket.Net.Models.Core.Users;

namespace Bitbucket.Net.Models.Core.Projects
{
    public class PullRequestActivity
    {
        public int Id { get; set; }
        public int CreatedDate { get; set; }
        public User User { get; set; }
        public string Action { get; set; }
        public string CommentAction { get; set; }
        public Comment Comment { get; set; }
        public CommentAnchor CommentAnchor { get; set; }
    }
}
