using System.Threading.Tasks;
using Bitbucket.Net.Models.Builds;
using Xunit;

namespace Bitbucket.Net.Tests
{
    public partial class BitbucketClientShould
    {
        [Theory]
        [InlineData("d81068c932ff3df1789ec9fbbec7d5dc3c15a050")]
        public async Task GetBuildStatsForCommitAsync(string commitId)
        {
            var result = await _client.GetBuildStatsForCommitAsync(commitId).ConfigureAwait(false);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("d81068c932ff3df1789ec9fbbec7d5dc3c15a050")]
        public async Task GetBuildStatsForCommitsAsync(params string[] commitIds)
        {
            var result = await _client.GetBuildStatsForCommitsAsync(commitIds).ConfigureAwait(false);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("d81068c932ff3df1789ec9fbbec7d5dc3c15a050")]
        public async Task GetBuildStatusForCommitAsync(string commitId)
        {
            var results = await _client.GetBuildStatusForCommitAsync(commitId).ConfigureAwait(false);
            Assert.NotEmpty(results);
        }

        [Theory]
        [InlineData("d81068c932ff3df1789ec9fbbec7d5dc3c15a050")]
        public async Task AssociateBuildStatusWithCommitAsync(string commitId)
        {
            var buildStatus = new BuildStatus
            {
                State = "SUCCESSFUL",
                Key = "some-key",
                Url = "http://some.successful.build"
            };

            var result = await _client.AssociateBuildStatusWithCommitAsync(commitId, buildStatus).ConfigureAwait(false);
            Assert.True(result);
        }
    }
}
