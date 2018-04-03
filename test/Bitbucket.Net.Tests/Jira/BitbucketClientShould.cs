using System.Threading.Tasks;
using Xunit;

namespace Bitbucket.Net.Tests
{
    public partial class BitbucketClientShould
    {
        [Theory]
        [InlineData("CD-123")]
        public async Task GetChangeSetsAsync(string issueKey)
        {
            var results = await _client.GetChangeSetsAsync(issueKey).ConfigureAwait(false);
            Assert.NotNull(results);
        }

        [Theory]
        [InlineData("Tools", "Test", 1)]
        public async Task GetJiraIssuesAsync(string projectKey, string repositorySlug, long pullRequestId)
        {
            var results = await _client.GetJiraIssuesAsync(projectKey, repositorySlug, pullRequestId).ConfigureAwait(false);
            Assert.NotNull(results);
        }
    }
}
