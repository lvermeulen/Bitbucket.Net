using System.Threading.Tasks;
using Xunit;

namespace Bitbucket.Net.Tests
{
    public partial class BitbucketClientShould
    {
        [Fact]
        public async Task GetClusterAsync()
        {
            var result = await _client.GetClusterAsync().ConfigureAwait(false);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetLicenseAsync()
        {
            var result = await _client.GetLicenseAsync().ConfigureAwait(false);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetMailServerAsync()
        {
            var result = await _client.GetMailServerAsync().ConfigureAwait(false);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetMailServerSenderAddressAsync()
        {
            string result = await _client.GetMailServerSenderAddressAsync().ConfigureAwait(false);
            Assert.Equal("noreply@test.com", result);
        }
    }
}
