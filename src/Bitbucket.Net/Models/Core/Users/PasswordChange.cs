using Bitbucket.Net.Models.Core.Admin;

namespace Bitbucket.Net.Models.Core.Users
{
    public class PasswordChange : PasswordBasic
    {
        public string OldPassword { get; set; }
    }
}
