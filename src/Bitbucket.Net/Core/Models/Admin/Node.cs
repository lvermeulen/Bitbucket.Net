namespace Bitbucket.Net.Core.Models.Admin
{
    public class Node
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public bool Local { get; set; }
    }
}