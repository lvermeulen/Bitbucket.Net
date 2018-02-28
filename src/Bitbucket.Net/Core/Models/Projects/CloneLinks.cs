using System.Collections.Generic;

namespace Bitbucket.Net.Core.Models.Projects
{
    public class CloneLinks : Links
    {
        public List<CloneLink> Clone { get; set; }
    }
}
