namespace Bitbucket.Net.Models
{
    public class Link
    {
        public string Href { get; set; }

        public override string ToString() => Href;
    }
}