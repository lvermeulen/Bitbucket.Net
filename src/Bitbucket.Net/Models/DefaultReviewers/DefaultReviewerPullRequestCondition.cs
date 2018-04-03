using System.Collections.Generic;
using Bitbucket.Net.Models.Core.Users;

namespace Bitbucket.Net.Models.DefaultReviewers
{
    public class DefaultReviewerPullRequestCondition
    {
        public int Id { get; set; }
        public DefaultReviewerPullRequestConditionScope Scope { get; set; }
        public RefMatcher SourceRefMatcher { get; set; }
        public RefMatcher TargetRefMatcher { get; set; }
        public List<User> Reviewers { get; set; }
        public int RequiredApprovals { get; set; }
    }
}
