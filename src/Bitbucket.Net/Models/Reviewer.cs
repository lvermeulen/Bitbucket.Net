namespace Bitbucket.Net.Models
{
    public class Reviewer : PullRequestUser
    {
        public string LastReviewedCommit { get; set; }
    }
}