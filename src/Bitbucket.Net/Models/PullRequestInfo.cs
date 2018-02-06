using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Bitbucket.Net.Models
{
    public class PullRequestInfo
    {
        public string Title { get; set; }
        public string Description { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public PullRequestState State { get; set; }
        public bool Open { get; set; }
        public bool Closed { get; set; }
        public FromToRef FromRef { get; set; }
        public FromToRef ToRef { get; set; }
        public bool Locked { get; set; }
        public List<Reviewer> Reviewers { get; set; }
    }
}
