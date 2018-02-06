using System.Collections.Generic;

namespace Bitbucket.Net.Models
{
    public class PullRequest : PullRequestInfo
    {
        public int Id { get; set; }
        public int Version { get; set; }
        //[JsonConverter(typeof(UnixDateTimeOffsetConverter))]
        public long CreatedDate { get; set; }
        //[JsonConverter(typeof(UnixDateTimeOffsetConverter))]
        public long UpdatedDate { get; set; }
        public PullRequestUser Author { get; set; }
        public List<PullRequestUser> Participants { get; set; }
        public Links Links { get; set; }

        public override string ToString() => $"{Author.User.DisplayName}: {Title ?? "(untitled)"}";
    }
}
