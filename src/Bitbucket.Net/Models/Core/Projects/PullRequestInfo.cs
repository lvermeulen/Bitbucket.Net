using System.Collections.Generic;
using Bitbucket.Net.Common.Converters;
using Newtonsoft.Json;

namespace Bitbucket.Net.Models.Core.Projects
{
    public class PullRequestInfo
    {
        public string Title { get; set; }
        public string Description { get; set; }
        [JsonConverter(typeof(PullRequestStatesConverter))]
        public PullRequestStates State { get; set; }
        public bool Open { get; set; }
        public bool Closed { get; set; }
        public FromToRef FromRef { get; set; }
        public FromToRef ToRef { get; set; }
        public bool Locked { get; set; }
        public List<Reviewer> Reviewers { get; set; }
    }
}
