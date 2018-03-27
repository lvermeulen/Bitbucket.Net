using System.Collections.Generic;

namespace Bitbucket.Net.Models.Core.Admin
{
    public class GroupUsers
    {
        public string Group { get; set; }
        public List<string> Users { get; set; }
    }
}
