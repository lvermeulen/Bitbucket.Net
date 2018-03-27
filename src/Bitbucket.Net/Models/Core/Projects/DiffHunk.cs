using System.Collections.Generic;

namespace Bitbucket.Net.Models.Core.Projects
{
    public class DiffHunk
    {
        public int SourceLine { get; set; }
        public int SourceSpan { get; set; }
        public int DestinationLine { get; set; }
        public int DestinationSpan { get; set; }
        public List<Segment> Segments { get; set; }
        public bool Truncated { get; set; }
    }
}