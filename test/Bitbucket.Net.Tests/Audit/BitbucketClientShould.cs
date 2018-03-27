using System.Threading.Tasks;
using Xunit;

namespace Bitbucket.Net.Tests
{
    public partial class BitbucketClientShould
    {
        [Theory]
        [InlineData("Tools")]
        public async Task GetProjectAuditEventsAsync(string projectKey)
        {
            var results = await _client.GetProjectAuditEventsAsync(projectKey).ConfigureAwait(false);
            Assert.NotEmpty(results);
        }

        [Theory]
        [InlineData("Tools", "Test")]
        public async Task GetProjectReposAuditEventsAsync(string projectKey, string repositorySlug)
        {
            var results = await _client.GetProjectRepoAuditEventsAsync(projectKey, repositorySlug).ConfigureAwait(false);
            Assert.NotEmpty(results);
        }
    }
}
