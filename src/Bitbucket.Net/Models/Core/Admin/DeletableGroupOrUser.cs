using Bitbucket.Net.Models.Core.Users;

namespace Bitbucket.Net.Models.Core.Admin
{
    public class DeletableGroupOrUser : Named
    {
        public bool Deletable { get; set; }
    }
}
