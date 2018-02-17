using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Bitbucket.Net.Tests
{
    public partial class BitbucketClientShould
    {
        [Fact]
        public async Task GetUsersAsync()
        {
            var result = await _client.GetUsersAsync().ConfigureAwait(false);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetUserAsync()
        {
            var firstUser = (await _client.GetUsersAsync().ConfigureAwait(false)).First();
            var result = await _client.GetUserAsync(firstUser.Slug).ConfigureAwait(false);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetUserSettingsAsync()
        {
            var firstUser = (await _client.GetUsersAsync().ConfigureAwait(false)).First();
            var result = await _client.GetUserSettingsAsync(firstUser.Slug).ConfigureAwait(false);
            Assert.NotNull(result);
        }
    }
}
