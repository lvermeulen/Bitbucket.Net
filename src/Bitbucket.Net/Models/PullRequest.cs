using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Bitbucket.Net.Models
{
    public class PullRequest
    {
        public int Id { get; set; }
        public int Version { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public PullRequestState State { get; set; }
        public bool Open { get; set; }
        public bool Closed { get; set; }
        //[JsonConverter(typeof(UnixDateTimeOffsetConverter))]
        public long CreatedDate { get; set; }
        //[JsonConverter(typeof(UnixDateTimeOffsetConverter))]
        public long UpdatedDate { get; set; }
        public FromToRef FromRef { get; set; }
        public FromToRef ToRef { get; set; }
        public bool Locked { get; set; }
        public PullRequestUser Author { get; set; }
        public List<Reviewer> Reviewers { get; set; }
        public List<PullRequestUser> Participants { get; set; }
        public Links Links { get; set; }

        public override string ToString() => $"{Author.User.DisplayName}: {Title ?? "(untitled)"}";
    }

    public enum PullRequestDirection
    {
        Incoming,
        Outgoing
    }

    public enum PullRequestOrder
    {
        Newest,
        Oldest
    }
    public enum PullRequestState
    {
        Open,
        Declined,
        Merged,
        All
    }
}
