using System.Collections.Generic;

namespace Bitbucket.Net.Models.Core.Projects
{
    public class Differences : DiffInfo
    {
        public List<Diff> Diffs { get; set; }
    }
}
