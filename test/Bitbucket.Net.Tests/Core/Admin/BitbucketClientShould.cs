using System.Collections.Generic;
using System.Threading.Tasks;
using Bitbucket.Net.Models.Core.Admin;
using Xunit;

namespace Bitbucket.Net.Tests
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
            bool result = await _client.AddAdminGroupUsersAsync(new GroupUsers
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
            Assert.NotNull(results);
        }

        [Fact]
        public async Task GetAdminUsersAsync()
        {
            var results = await _client.GetAdminUsersAsync().ConfigureAwait(false);
            Assert.NotEmpty(results);
        }

        [Fact]
        public async Task GetAdminUserMoreMembersAsync()
        {
            var results = await _client.GetAdminUserMoreMembersAsync("lvermeulen");
            Assert.NotEmpty(results);
        }

        [Fact]
        public async Task GetAdminUserMoreNonMembersAsync()
        {
            var results = await _client.GetAdminUserMoreNonMembersAsync("lvermeulen");
            Assert.NotNull(results);
        }

        [Fact]
        public async Task GetAdminGroupPermissionsAsync()
        {
            var results = await _client.GetAdminGroupPermissionsAsync();
            Assert.NotEmpty(results);
        }

        [Fact]
        public async Task GetAdminGroupPermissionsNoneAsync()
        {
            var results = await _client.GetAdminGroupPermissionsNoneAsync();
            Assert.NotNull(results);
        }

        [Fact]
        public async Task GetAdminUserPermissionsAsync()
        {
            var results = await _client.GetAdminUserPermissionsAsync();
            Assert.NotEmpty(results);
        }

        [Fact]
        public async Task GetAdminUserPermissionsNoneAsync()
        {
            var results = await _client.GetAdminUserPermissionsNoneAsync();
            Assert.NotNull(results);
        }

        [Fact]
        public async Task GetAdminMergeStrategiesAsync()
        {
            var result = await _client.GetAdminPullRequestsMergeStrategiesAsync("git").ConfigureAwait(false);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetAdminClusterAsync()
        {
            var result = await _client.GetAdminClusterAsync().ConfigureAwait(false);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetAdminLicenseAsync()
        {
            var result = await _client.GetAdminLicenseAsync().ConfigureAwait(false);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetAdminMailServerAsync()
        {
            var result = await _client.GetAdminMailServerAsync().ConfigureAwait(false);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetAdminMailServerSenderAddressAsync()
        {
            string result = await _client.GetAdminMailServerSenderAddressAsync().ConfigureAwait(false);
            Assert.Equal("noreply@test.com", result);
        }
    }
}
