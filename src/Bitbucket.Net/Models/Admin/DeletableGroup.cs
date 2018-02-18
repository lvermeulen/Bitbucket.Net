using Bitbucket.Net.Models.Users;

namespace Bitbucket.Net.Models.Admin
{
    public class DeletableGroup : Named
    {
        public bool Deletable { get; set; }
    }
}
