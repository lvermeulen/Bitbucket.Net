using System.Collections.Generic;

namespace Bitbucket.Net.Models.Git
{
    public class RebasePullRequestCondition
    {
        public bool CanRebase { get; set; }
        public bool CanWrite { get; set; }
        public List<Veto> Vetoes { get; set; }
    }
}
