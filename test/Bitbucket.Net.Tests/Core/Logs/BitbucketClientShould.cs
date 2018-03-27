using System.Threading.Tasks;
using Bitbucket.Net.Models.Core.Logs;
using Xunit;

namespace Bitbucket.Net.Tests
{
    public partial class BitbucketClientShould
    {
        [Fact]
        public async Task GetRootLogLevelAsync()
        {
            var logLevel = await _client.GetRootLogLevelAsync().ConfigureAwait(false);
            Assert.Equal(LogLevels.Warn, logLevel);
        }
    }
}
