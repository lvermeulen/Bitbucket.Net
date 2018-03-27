namespace Bitbucket.Net.Models.Core.Projects
{
    public class Project : ProjectDefinition
    {
        public int Id { get; set; }
        public bool Public { get; set; }
        public string Type { get; set; }
        public Links Links { get; set; }

        public override string ToString() => Name;
    }
}
