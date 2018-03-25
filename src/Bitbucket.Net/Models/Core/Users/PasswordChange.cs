using Bitbucket.Net.Models.Admin;

namespace Bitbucket.Net.Models.Users
{
    public class PasswordChange : PasswordBasic
    {
        public string OldPassword { get; set; }
    }
}
