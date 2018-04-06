using System.Threading.Tasks;
using Xunit;

namespace Bitbucket.Net.Tests
{
    public partial class BitbucketClientShould
    {
        [Theory]
        [InlineData("Tools")]
        public async Task GetProjectRefRestrictionsAsync(string projectKey)
        {
            var results = await _client.GetProjectRefRestrictionsAsync(projectKey).ConfigureAwait(false);
            Assert.NotEmpty(results);
        }

        [Theory]
        [InlineData("Tools", 3)]
        public async Task GetProjectRefRestrictionAsync(string projectKey, int refRestrictionId)
        {
            var result = await _client.GetProjectRefRestrictionAsync(projectKey, refRestrictionId).ConfigureAwait(false);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("Tools", "Test")]
        public async Task GetRepositoryRefRestrictionsAsync(string projectKey, string repositorySlug)
        {
            var results = await _client.GetRepositoryRefRestrictionsAsync(projectKey, repositorySlug).ConfigureAwait(false);
            Assert.NotEmpty(results);
        }

        [Theory]
        [InlineData("Tools", "Test", 3)]
        public async Task GetRepositoryRefRestrictionAsync(string projectKey, string repositorySlug, int refRestrictionId)
        {
            var result = await _client.GetRepositoryRefRestrictionAsync(projectKey, repositorySlug, refRestrictionId).ConfigureAwait(false);
            Assert.NotNull(result);
        }
    }
}
