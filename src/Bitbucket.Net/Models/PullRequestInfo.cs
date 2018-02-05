using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Bitbucket.Net.Models
{
    public class PullRequestInfo
    {
        [JsonProperty("Title")]
        public string Title { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("state")]
        public PullRequestState State { get; set; }
        [JsonProperty("open")]
        public bool Open { get; set; }
        [JsonProperty("closed")]
        public bool Closed { get; set; }
        [JsonProperty("fromRef")]
        public FromToRef FromRef { get; set; }
        [JsonProperty("toRef")]
        public FromToRef ToRef { get; set; }
        [JsonProperty("locked")]
        public bool Locked { get; set; }
        [JsonProperty("reviewers")]
        public List<Reviewer> Reviewers { get; set; }
    }
}
