using System.Threading.Tasks;
using Xunit;

namespace Bitbucket.Net.Tests
{
    public partial class BitbucketClientShould
    {
        [Fact]
        public async Task GetApplicationPropertiesAsync()
        {
            var results = await _client.GetApplicationPropertiesAsync().ConfigureAwait(false);
            Assert.NotEmpty(results);
        }
    }
}
