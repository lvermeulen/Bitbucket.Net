using Bitbucket.Net.Models.Users;

namespace Bitbucket.Net.Models.Admin
{
    public class DeletableGroupOrUser : Named
    {
        public bool Deletable { get; set; }
    }
}
