using System.Collections.Generic;

namespace Bitbucket.Net.Models.Projects
{
    public class Differences : DiffInfo
    {
        public List<Diff> Diffs { get; set; }
    }
}
