using System.Threading.Tasks;
using Xunit;

namespace Bitbucket.Net.Tests
{
    public partial class BitbucketClientShould
    {
        [Theory]
        [InlineData("Tools")]
        public async Task GetProjectDefaultReviewerConditionsAsync(string projectKey)
        {
            var results = await _client.GetDefaultReviewerConditionsAsync(projectKey).ConfigureAwait(false);
            Assert.NotEmpty(results);
        }

        [Theory]
        [InlineData("Tools", "Test")]
        public async Task GetProjectRepoDefaultReviewerConditionsAsync(string projectKey, string repositorySlug)
        {
            var results = await _client.GetDefaultReviewerConditionsAsync(projectKey, repositorySlug).ConfigureAwait(false);
            Assert.NotEmpty(results);
        }

        [Theory]
        [InlineData("Tools", "Test")]
        public async Task GetDefaultReviewersAsync(string projectKey, string repositorySlug)
        {
            var results = await _client.GetDefaultReviewerConditionsAsync(projectKey, repositorySlug).ConfigureAwait(false);
            Assert.NotEmpty(results);
        }
    }
}
