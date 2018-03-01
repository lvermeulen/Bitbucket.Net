using System.Threading.Tasks;
using Xunit;

namespace Bitbucket.Net.Core.Tests
{
    public partial class BitbucketClientShould
    {
        [Fact]
        public async Task GetDashboardPullRequestsAsync()
        {
            var results = await _client.GetDashboardPullRequestsAsync().ConfigureAwait(false);
            Assert.NotNull(results);
        }

        [Fact]
        public async Task GetDashboardPullRequestSuggestionsAsync()
        {
            var results = await _client.GetDashboardPullRequestSuggestionsAsync().ConfigureAwait(false);
            Assert.NotNull(results);
        }
    }
}
