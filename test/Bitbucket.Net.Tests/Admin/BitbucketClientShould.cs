using System.Collections.Generic;
using System.Threading.Tasks;
using Bitbucket.Net.Core.Models.Admin;
using Xunit;

namespace Bitbucket.Net.Core.Tests
{
    public partial class BitbucketClientShould
    {
        [Fact]
        public async Task GetAdminGroupsAsync()
        {
            var results = await _client.GetAdminGroupsAsync().ConfigureAwait(false);
            Assert.NotEmpty(results);
        }

        [Fact]
        public async Task AddAdminGroupUsersAsync()
        {
            var result = await _client.AddAdminGroupUsersAsync(new UsersGroup
            {
                Group = "stash-users",
                Users = new List<string> { "lvermeulen" }
            }).ConfigureAwait(false);

            Assert.True(result);
        }

        [Fact]
        public async Task GetAdminGroupMoreMembersAsync()
        {
            var results = await _client.GetAdminGroupMoreMembersAsync("stash-users").ConfigureAwait(false);
            Assert.NotEmpty(results);
        }

        [Fact]
        public async Task GetAdminGroupMoreNonMembersAsync()
        {
            var results = await _client.GetAdminGroupMoreNonMembersAsync("stash-users").ConfigureAwait(false);
            Assert.NotEmpty(results);
        }

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
