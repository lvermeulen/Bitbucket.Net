using System.Threading.Tasks;
using Xunit;

namespace Bitbucket.Net.Tests
{
    public partial class BitbucketClientShould
    {
        [Theory]
        [InlineData("lvermeulen")]
        public async Task GetUserAccessTokensAsync(string userSlug)
        {
            var results = await _client.GetUserAccessTokensAsync(userSlug).ConfigureAwait(false);
            Assert.NotEmpty(results);
        }

        [Theory]
        [InlineData("lvermeulen", "042678046727")]
        public async Task GetUserAccessTokenAsync(string userSlug, string tokenId)
        {
            var result = await _client.GetUserAccessTokenAsync(userSlug, tokenId).ConfigureAwait(false);
            Assert.NotNull(result);
        }
    }
}
