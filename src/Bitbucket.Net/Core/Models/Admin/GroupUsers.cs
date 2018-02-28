using System.Collections.Generic;

namespace Bitbucket.Net.Core.Models.Admin
{
    public class GroupUsers
    {
        public string Group { get; set; }
        public List<string> Users { get; set; }
    }
}
