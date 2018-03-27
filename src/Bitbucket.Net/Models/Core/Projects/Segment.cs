using System.Collections.Generic;

namespace Bitbucket.Net.Models.Core.Projects
{
    public class Segment
    {
        public string Type { get; set; }
        public List<LineRef> Lines { get; set; }
        public bool Truncated { get; set; }
    }
}