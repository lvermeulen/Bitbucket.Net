namespace Bitbucket.Net.Core.Models.Users
{
    public class PasswordChange
    {
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public string OldPassword { get; set; }
    }

}
