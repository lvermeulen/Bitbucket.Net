namespace Bitbucket.Net.Core.Models.Projects
{
    public class Link
    {
        public string Href { get; set; }

        public override string ToString() => Href;
    }
}