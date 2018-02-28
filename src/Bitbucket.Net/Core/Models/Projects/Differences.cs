using System.Collections.Generic;

namespace Bitbucket.Net.Core.Models.Projects
{
    public class Differences : DiffInfo
    {
        public List<Diff> Diffs { get; set; }
    }
}
