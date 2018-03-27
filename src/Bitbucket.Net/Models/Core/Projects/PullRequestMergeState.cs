using System.Collections.Generic;

namespace Bitbucket.Net.Models.Core.Projects
{
    public class PullRequestMergeState
    {
        public bool CanMerge { get; set; }
        public bool Conflicted { get; set; }
        public string Outcome { get; set; }
        public List<Veto> Vetoes { get; set; }
    }
}
