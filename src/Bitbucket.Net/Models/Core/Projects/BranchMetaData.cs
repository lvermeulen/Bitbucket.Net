using Newtonsoft.Json;

namespace Bitbucket.Net.Models.Core.Projects
{
    public class BranchMetaData
    {
        [JsonProperty("com.atlassian.bitbucket.server.bitbucket-branch:ahead-behind-metadata-provider")]
        public AheadBehindMetaData AheadBehind { get; set; }

        [JsonProperty("com.atlassian.bitbucket.server.bitbucket-build:build-status-metadata")]
        public BuildStatusMetadata BuildStatus { get; set; }

        [JsonProperty("com.atlassian.bitbucket.server.bitbucket-ref-metadata:outgoing-pull-request-metadata")]
        public PullRequestMetadata OutgoingPullRequest { get; set; }
    }
}
