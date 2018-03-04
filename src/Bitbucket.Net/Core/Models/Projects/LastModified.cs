using System.Collections.Generic;

namespace Bitbucket.Net.Core.Models.Projects
{
    public class LastModified
    {
        public Dictionary<string, Commit> Files { get; set; }
        public Commit LatestCommit { get; set; }
    }
}
