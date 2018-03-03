using System.Threading.Tasks;
using Xunit;

namespace Bitbucket.Net.Core.Tests
{
    public partial class BitbucketClientShould
    {
        [Fact]
        public async Task GetRepositoriesAsync()
        {
            var result = await _client.GetRepositoriesAsync().ConfigureAwait(false);
            Assert.NotNull(result);
        }
    }
}
