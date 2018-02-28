using System.Threading.Tasks;
using Xunit;

namespace Bitbucket.Net.Core.Tests
{
    public partial class BitbucketClientShould
    {
        [Fact]
        public async Task PreviewMarkupAsync()
        {
            string result = await _client.PreviewMarkupAsync("# Hello World!").ConfigureAwait(false);
            Assert.Equal("<h1>Hello World!</h1>\n", result);
        }
    }
}
