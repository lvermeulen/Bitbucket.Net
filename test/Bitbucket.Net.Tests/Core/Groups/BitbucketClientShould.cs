using System.Threading.Tasks;
using Xunit;

namespace Bitbucket.Net.Tests
{
    public partial class BitbucketClientShould
    {
        [Fact]
        public async Task GetGroupNamesAsync()
        {
            var results = await _client.GetGroupNamesAsync().ConfigureAwait(false);
            Assert.NotNull(results);
        }
    }
}
