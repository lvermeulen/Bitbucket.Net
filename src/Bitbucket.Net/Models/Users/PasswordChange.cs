namespace Bitbucket.Net.Models.Users
{
    public class PasswordChange
    {
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public string OldPassword { get; set; }
    }

}
