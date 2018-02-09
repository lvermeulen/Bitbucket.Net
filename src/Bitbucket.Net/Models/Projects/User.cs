namespace Bitbucket.Net.Models.Projects
{
    public class User
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public bool Active { get; set; }
        public string Slug { get; set; }
        public string Type { get; set; }

        public override string ToString() => DisplayName;
    }
}