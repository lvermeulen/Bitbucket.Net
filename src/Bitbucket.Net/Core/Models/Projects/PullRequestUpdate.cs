using System.Collections.Generic;

namespace Bitbucket.Net.Core.Models.Projects
{
    public class PullRequestUpdate
    {
        public int Id { get; set; }
        public int Version { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Reviewer> Reviewers { get; set; }
    }
}
