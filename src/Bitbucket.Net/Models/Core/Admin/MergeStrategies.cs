using System.Collections.Generic;

namespace Bitbucket.Net.Models.Core.Admin
{
    public class MergeStrategies
    {
        public MergeStrategy DefaultStrategy { get; set; }

        public List<MergeStrategy> Strategies { get; set; }

        public string Type { get; set; }
    }
}
