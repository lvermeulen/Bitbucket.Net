using System.Collections.Generic;

namespace Bitbucket.Net.Core.Models.Admin
{
    public class Cluster
    {
        public Node LocalNode { get; set; }
        public List<Node> Nodes { get; set; }
        public bool Running { get; set; }
    }
}
