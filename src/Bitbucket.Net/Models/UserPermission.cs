namespace Bitbucket.Net.Models
{
    public class UserPermission
    {
        public User User { get; set; }
        public string Permission { get; set; }

        public override string ToString() => $"{Permission} - {User?.DisplayName}";
    }
}
