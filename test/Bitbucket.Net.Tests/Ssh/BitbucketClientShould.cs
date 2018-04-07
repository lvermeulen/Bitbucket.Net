using System.Threading.Tasks;
using Xunit;

namespace Bitbucket.Net.Tests
{
    public partial class BitbucketClientShould
    {
        [Theory]
        [InlineData(3)]
        public async Task GetProjectKeysForKeyAsync(int keyId)
        {
            var results = await _client.GetProjectKeysAsync(keyId).ConfigureAwait(false);
            Assert.NotEmpty(results);
        }

        [Theory]
        [InlineData("Tools")]
        public async Task GetProjectKeysForProjectAsync(string projectKey)
        {
            var results = await _client.GetProjectKeysAsync(projectKey).ConfigureAwait(false);
            Assert.NotEmpty(results);
        }

        [Theory]
        [InlineData("Tools", 3)]
        public async Task GetProjectKeyAsync(string projectKey, int keyId)
        {
            var result = await _client.GetProjectKeyAsync(projectKey, keyId);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(4)]
        public async Task GetRepoKeysForKeyAsync(int keyId)
        {
            var results = await _client.GetRepoKeysAsync(keyId).ConfigureAwait(false);
            Assert.NotEmpty(results);
        }

        [Theory]
        [InlineData("Tools", "Test")]
        public async Task GetRepoKeysForRepoAsync(string projectKey, string repositorySlug)
        {
            var results = await _client.GetRepoKeysAsync(projectKey, repositorySlug).ConfigureAwait(false);
            Assert.NotEmpty(results);
        }

        [Theory]
        [InlineData("Tools", "Test", 4)]
        public async Task GetRepoKeyAsync(string projectKey, string repositorySlug, int keyId)
        {
            var result = await _client.GetRepoKeyAsync(projectKey, repositorySlug, keyId).ConfigureAwait(false);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetUserKeysAsync()
        {
            var results = await _client.GetUserKeysAsync().ConfigureAwait(false);
            Assert.NotEmpty(results);
        }

        [Fact]
        public async Task GetSshSettingsAsync()
        {
            var result = await _client.GetSshSettingsAsync().ConfigureAwait(false);
            Assert.NotNull(result);
        }
    }
}
