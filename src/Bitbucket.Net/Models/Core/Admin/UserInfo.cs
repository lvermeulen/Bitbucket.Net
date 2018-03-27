using Bitbucket.Net.Models.Core.Users;

namespace Bitbucket.Net.Models.Core.Admin
{
    public class UserInfo : User
    {
        public string DirectoryName { get; set; }
        public bool Deletable { get; set; }
        public long LastAuthenticationTimestamp { get; set; }
        public bool MutableDetails { get; set; }
        public bool MutableGroups { get; set; }
    }

}
