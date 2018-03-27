namespace Bitbucket.Net.Models.Core.Users
{
    public class Named
    {
        public string Name { get; set; }

        public override string ToString() => Name;
    }
}
