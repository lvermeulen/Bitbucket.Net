using Newtonsoft.Json;

namespace Bitbucket.Net.Models.Core.Projects
{
    public class Branch : BranchBase
    {
        private BranchMetaData _branchMetadata;

        public string LatestCommit { get; set; }
        public string LatestChangeset { get; set; }
        public bool IsDefault { get; set; }

        public BranchMetaData BranchMetadata
        {
            get
            {
                if (_branchMetadata != null)
                {
                    return _branchMetadata;
                }

                if (Metadata == null)
                {
                    return null;
                }

                _branchMetadata = new BranchMetaData();

                foreach (var metadata in Metadata)
                {
                    if (metadata.Name.ToString() == "com.atlassian.bitbucket.server.bitbucket-branch:ahead-behind-metadata-provider")
                    {
                        _branchMetadata.AheadBehind = JsonConvert.DeserializeObject<AheadBehindMetaData>(metadata.Value.ToString());
                    }
                    else if (metadata.Name.ToString() == "com.atlassian.bitbucket.server.bitbucket-build:build-status-metadata")
                    {
                        _branchMetadata.BuildStatus = JsonConvert.DeserializeObject<BuildStatusMetadata>(metadata.Value.ToString());
                    }
                    else if (metadata.Name.ToString() == "com.atlassian.bitbucket.server.bitbucket-ref-metadata:outgoing-pull-request-metadata")
                    {
                        _branchMetadata.OutgoingPullRequest = JsonConvert.DeserializeObject<PullRequestMetadata>(metadata.Value.ToString());
                    }
                }

                return _branchMetadata;
            }
        }

        public dynamic Metadata { get; set; }

        public override string ToString() => DisplayId;
    }
}
