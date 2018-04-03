using System.Threading.Tasks;
using Xunit;

namespace Bitbucket.Net.Tests
{
    public partial class BitbucketClientShould
    {
        [Theory]
        [InlineData("Tools", "Test", 1)]
        public async Task GetCanRebasePullRequestAsync(string projectKey, string repositorySlug, long pullRequestId)
        {
            var result = await _client.GetCanRebasePullRequestAsync(projectKey, repositorySlug, pullRequestId).ConfigureAwait(false);
            Assert.NotNull(result);
        }
    }
}
