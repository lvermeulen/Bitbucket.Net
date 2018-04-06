using System.Threading.Tasks;
using Xunit;

namespace Bitbucket.Net.Tests
{
    public partial class BitbucketClientShould
    {
        [Theory]
        [InlineData("Tools", "Test")]
        public async Task GetRepositorySynchronizationStatusAsync(string projectKey, string repositorySlug)
        {
            var result = await _client.GetRepositorySynchronizationStatusAsync(projectKey, repositorySlug).ConfigureAwait(false);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("Tools", "Test", true)]
        public async Task EnableRepositorySynchronizationAsync(string projectKey, string repositorySlug, bool enabled)
        {
            var result = await _client.EnableRepositorySynchronizationAsync(projectKey, repositorySlug, enabled).ConfigureAwait(false);
            Assert.NotNull(result);
        }
    }
}
