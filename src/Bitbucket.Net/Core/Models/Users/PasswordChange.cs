using Bitbucket.Net.Core.Models.Admin;

namespace Bitbucket.Net.Core.Models.Users
{
    public class PasswordChange : PasswordBasic
    {
        public string OldPassword { get; set; }
    }
}
