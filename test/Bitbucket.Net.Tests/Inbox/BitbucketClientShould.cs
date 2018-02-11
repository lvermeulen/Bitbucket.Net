using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Bitbucket.Net.Tests
{
    public partial class BitbucketClientShould
    {
        [Fact]
        public async Task GetInboxPullRequestsAsync()
        {
            var results = await _client.GetInboxPullRequestsAsync(role: Models.Projects.Roles.Author).ConfigureAwait(false);
            Assert.True(results.Any());
        }

        [Fact]
        public async Task GetInboxPullRequestsCountAsync()
        {
            int result = await _client.GetInboxPullRequestsCountAsync().ConfigureAwait(false);
            Assert.True(result >= 0);
        }
    }
}
