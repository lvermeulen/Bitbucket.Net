using System.Linq;
using System.Threading.Tasks;
using Bitbucket.Net.Core.Models.Projects;
using Xunit;

namespace Bitbucket.Net.Core.Tests
{
    public partial class BitbucketClientShould
    {
        [Fact]
        public async Task GetInboxPullRequestsAsync()
        {
            var results = await _client.GetInboxPullRequestsAsync(role: Roles.Author).ConfigureAwait(false);
            Assert.True(Enumerable.Any(results));
        }

        [Fact]
        public async Task GetInboxPullRequestsCountAsync()
        {
            int result = await _client.GetInboxPullRequestsCountAsync().ConfigureAwait(false);
            Assert.True(result >= 0);
        }
    }
}
