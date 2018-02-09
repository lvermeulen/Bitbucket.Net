using System.Collections.Generic;

namespace Bitbucket.Net.Models.Admin
{
    public class Cluster
    {
        public Node LocalNode { get; set; }
        public List<Node> Nodes { get; set; }
        public bool Running { get; set; }
    }
}
