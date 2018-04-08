using System.Threading.Tasks;
using Xunit;

namespace Bitbucket.Net.Tests
{
    public partial class BitbucketClientShould
    {
        [Fact]
        public async Task GetRecentReposAsync()
        {
            var results = await _client.GetRecentReposAsync().ConfigureAwait(false);
            Assert.NotEmpty(results);
        }
    }
}
