namespace Bitbucket.Net.Models.Core.Projects
{
    public class Link
    {
        public string Href { get; set; }

        public override string ToString() => Href;
    }
}