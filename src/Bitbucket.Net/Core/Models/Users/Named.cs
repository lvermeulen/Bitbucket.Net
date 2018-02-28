namespace Bitbucket.Net.Core.Models.Users
{
    public class Named
    {
        public string Name { get; set; }

        public override string ToString() => Name;
    }
}
