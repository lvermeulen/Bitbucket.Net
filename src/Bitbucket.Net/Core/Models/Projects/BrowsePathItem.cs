using System.Collections.Generic;
using Bitbucket.Net.Common.Models;

namespace Bitbucket.Net.Core.Models.Projects
{
    public class BrowsePathItem : PagedResultsBase
    {
        public List<Line> Lines { get; set; }
    }
}
