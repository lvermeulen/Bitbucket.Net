using System.Collections.Generic;

namespace Bitbucket.Net.Core.Models.Projects
{
    public class PullRequestMergeState
    {
        public bool CanMerge { get; set; }
        public bool Conflicted { get; set; }
        public string Outcome { get; set; }
        public List<Veto> Vetoes { get; set; }
    }
}
