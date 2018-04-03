namespace Bitbucket.Net.Models.DefaultReviewers
{
    public class RefMatcher
    {
        public bool Active { get; set; }
        public string Id { get; set; }
        public string DisplayId { get; set; }
        public DefaultReviewerPullRequestConditionType Type { get; set; }
    }
}