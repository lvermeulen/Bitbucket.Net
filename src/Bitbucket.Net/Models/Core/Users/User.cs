namespace Bitbucket.Net.Models.Core.Users
{
    public class User : Identity
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public bool Active { get; set; }
        public string Slug { get; set; }
        public string Type { get; set; }

        public override string ToString() => DisplayName;
    }
}