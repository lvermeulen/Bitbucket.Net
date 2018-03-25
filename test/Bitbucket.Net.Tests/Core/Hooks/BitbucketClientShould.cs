using System.Threading.Tasks;
using Xunit;

namespace Bitbucket.Net.Tests
{
    public partial class BitbucketClientShould
    {
        [Fact]
        public async Task GetProjectHooksAvatarAsync()
        {
            var result = await _client.GetProjectHooksAvatarAsync("com.atlassian.bitbucket.server.bitbucket-bundled-hooks:all-approvers-merge-check").ConfigureAwait(false);
            Assert.NotNull(result);
        }
    }
}
