namespace Bitbucket.Net.Models.Projects
{
    public class Reviewer : PullRequestUser
    {
        public string LastReviewedCommit { get; set; }
    }
}