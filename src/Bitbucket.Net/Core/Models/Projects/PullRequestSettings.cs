using Bitbucket.Net.Core.Models.Admin;
using Newtonsoft.Json;

namespace Bitbucket.Net.Core.Models.Projects
{
    public class PullRequestSettings
    {
        public MergeStrategies MergeConfig { get; set; }
        public bool RequiredAllApprovers { get; set; }
        public bool RequiredAllTasksComplete { get; set; }
        [JsonProperty("com.atlassian.bitbucket.server.bitbucket-bundled-hooks:requiredApprovers")]
        public MergeHookRequiredApprovers ComatlassianbitbucketserverbundledhooksrequiredApprovers { get; set; }
        public int RequiredApprovers { get; set; }
        [JsonProperty("com.atlassian.bitbucket.server.bitbucket-build:requiredBuilds")]
        public MergeCheckRequiredBuilds ComatlassianbitbucketserverbitbucketbuildrequiredBuilds { get; set; }
        public int RequiredSuccessfulBuilds { get; set; }
    }
}
