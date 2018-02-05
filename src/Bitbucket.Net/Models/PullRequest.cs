using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bitbucket.Net.Models
{
    public class PullRequest : PullRequestInfo
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("version")]
        public int Version { get; set; }
        //[JsonConverter(typeof(UnixDateTimeOffsetConverter))]
        [JsonProperty("createdDate")]
        public long CreatedDate { get; set; }
        //[JsonConverter(typeof(UnixDateTimeOffsetConverter))]
        [JsonProperty("updatedDate")]
        public long UpdatedDate { get; set; }
        [JsonProperty("author")]
        public PullRequestUser Author { get; set; }
        [JsonProperty("participants")]
        public List<PullRequestUser> Participants { get; set; }
        [JsonProperty("links")]
        public Links Links { get; set; }

        public override string ToString() => $"{Author.User.DisplayName}: {Title ?? "(untitled)"}";
    }
}
