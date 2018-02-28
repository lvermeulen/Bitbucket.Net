namespace Bitbucket.Net.Core.Models.Admin
{
    public abstract class PasswordBasic
    {
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
}
