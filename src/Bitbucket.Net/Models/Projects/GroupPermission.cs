namespace Bitbucket.Net.Models.Projects
{
    public class GroupPermission
    {
        public Group Group { get; set; }
        public string Permission { get; set; }

        public override string ToString() => $"{Permission} - {Group}";
    }
}
