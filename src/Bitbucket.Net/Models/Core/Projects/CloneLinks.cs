using System.Collections.Generic;

namespace Bitbucket.Net.Models.Core.Projects
{
    public class CloneLinks : Links
    {
        public List<CloneLink> Clone { get; set; }
    }
}
