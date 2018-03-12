namespace Bitbucket.Net.Common.Models
{
    public abstract class PagedResultsBase
    {
        public int Size { get; set; }
        public bool IsLastPage { get; set; }
        public int Start { get; set; }
    }
}