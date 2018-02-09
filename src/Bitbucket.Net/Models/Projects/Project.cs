namespace Bitbucket.Net.Models.Projects
{
    public class Project : ProjectRef
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Public { get; set; }
        public string Type { get; set; }
        public Links Links { get; set; }

        public override string ToString() => Name;
    }
}
