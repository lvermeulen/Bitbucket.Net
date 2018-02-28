using Bitbucket.Net.Core.Models.Users;

namespace Bitbucket.Net.Core.Models.Admin
{
    public class DeletableGroup : Named
    {
        public bool Deletable { get; set; }
    }
}
