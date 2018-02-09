namespace Bitbucket.Net.Models.Projects
{
    public class Link
    {
        public string Href { get; set; }

        public override string ToString() => Href;
    }
}