using System.Threading.Tasks;
using Xunit;

namespace Bitbucket.Net.Tests
{
    public partial class BitbucketClientShould
    {
        [Theory]
        [InlineData("Tools", "Test", "d81068c932ff3df1789ec9fbbec7d5dc3c15a050")]
        public async Task GetCommitBranchInfoAsync(string projectKey, string repositorySlug, string fullSha)
        {
            var results = await _client.GetCommitBranchInfoAsync(projectKey, repositorySlug, fullSha).ConfigureAwait(false);
            Assert.NotEmpty(results);
        }

        [Theory]
        [InlineData("Tools", "Test")]
        public async Task GetRepoBranchModelAsync(string projectKey, string repositorySlug)
        {
            var result = await _client.GetRepoBranchModelAsync(projectKey, repositorySlug).ConfigureAwait(false);
            Assert.NotNull(result);
        }
    }
}
