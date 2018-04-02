using System.Threading.Tasks;
using Xunit;

namespace Bitbucket.Net.Tests
{
    public partial class BitbucketClientShould
    {
        [Theory]
        [InlineData("Tools", "Test", "d81068c932ff3df1789ec9fbbec7d5dc3c15a050", "1")]
        public async Task GetCommentLikesAsync(string projectKey, string repositorySlug, string commitId, string commentId)
        {
            var result = await _client.GetCommitCommentLikesAsync(projectKey, repositorySlug, commitId, commentId).ConfigureAwait(false);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("Tools", "Test", "1", "1")]
        public async Task GetPullRequestCommentLikesAsync(string projectKey, string repositorySlug, string pullRequestId, string commentId)
        {
            var result = await _client.GetPullRequestCommentLikesAsync(projectKey, repositorySlug, pullRequestId, commentId).ConfigureAwait(false);
            Assert.NotNull(result);
        }
    }
}
