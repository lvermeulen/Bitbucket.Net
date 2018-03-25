using Bitbucket.Net.Common.Models;

namespace Bitbucket.Net.Models.Projects
{
    public class BrowseItem
    {
        public Path Path { get; set; }
        public string Revision { get; set; }
        public PagedResults<ContentItem> Children { get; set; }
    }
}
