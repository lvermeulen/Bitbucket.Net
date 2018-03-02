using System.Threading.Tasks;
using Xunit;

namespace Bitbucket.Net.Core.Tests
{
    public partial class BitbucketClientShould
    {
        [Fact]
        public async Task GetProjectHooksAvatarAsync()
        {
            var result = await _client.GetProjectHooksAvatarAsync("").ConfigureAwait(false);
            Assert.NotNull(result);
        }
    }
}
