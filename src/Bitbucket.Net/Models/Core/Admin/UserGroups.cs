using System.Collections.Generic;

namespace Bitbucket.Net.Models.Core.Admin
{
    public class UserGroups
    {
        public string User { get; set; }
        public List<string> Groups { get; set; }
    }
}
