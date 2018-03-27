using System;
using System.Linq;
using System.Threading.Tasks;
using Bitbucket.Net.Models.Core.Admin;
using Bitbucket.Net.Models.Core.Projects;
using Xunit;

namespace Bitbucket.Net.Tests
{
    public partial class BitbucketClientShould
    {
        [Fact]
        public async Task GetProjectsAsync()
        {
            var results = await _client.GetProjectsAsync(permission: Permissions.ProjectRead).ConfigureAwait(false);
            Assert.True(results.Any());
        }

        [Fact]
        public async Task IsProjectDefaultPermissionAsync()
        {
            bool result = await _client.IsProjectDefaultPermissionAsync("Tools", Permissions.ProjectRead);
            Assert.True(result);
        }

        [Theory]
        [InlineData("Tools", 1)]
        public async Task GetProjectRepositoriesAsync(string projectKey, int maxPages)
        {
            var results = await _client.GetProjectRepositoriesAsync(projectKey, maxPages: maxPages).ConfigureAwait(false);
            Assert.True(results.Any());
        }

        [Theory]
        [InlineData("Tools", "Test")]
        public async Task GetProjectRepositoryAsync(string projectKey, string repositorySlug)
        {
            var result = await _client.GetProjectRepositoryAsync(projectKey, repositorySlug).ConfigureAwait(false);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("Tools", "Test")]
        public async Task GetProjectRepositoryGroupPermissionsAsync(string projectKey, string repositorySlug)
        {
            var results = await _client.GetProjectRepositoryGroupPermissionsAsync(projectKey, repositorySlug).ConfigureAwait(false);
            Assert.True(results.Any());
        }

        [Theory]
        [InlineData("Tools", "Test")]
        public async Task GetProjectRepositoryUserPermissionsAsync(string projectKey, string repositorySlug)
        {
            var results = await _client.GetProjectRepositoryUserPermissionsAsync(projectKey, repositorySlug).ConfigureAwait(false);
            Assert.False(results.Any());
        }

        [Theory]
        [InlineData("Tools", "Test")]
        public async Task GetBranchesAsync(string projectKey, string repositorySlug)
        {
            var results = await _client.GetBranchesAsync(projectKey, repositorySlug, maxPages: 1).ConfigureAwait(false);
            Assert.True(results.Any());
        }

        [Theory]
        [InlineData("Tools", "Test", 3)]
        public async Task GetBranchesToDeleteAsync(string projectKey, string repositorySlug, int daysOlderThanToday)
        {
            var results = await _client.GetBranchesAsync(projectKey, repositorySlug, details: true).ConfigureAwait(false);
            var list = results.ToList();
            Assert.True(list.Any());

            var deleteStates = new[] { PullRequestStates.Merged, PullRequestStates.Declined };
            var branchesToDelete = list.Where(branch =>
                !branch.IsDefault
                && deleteStates.Any(state => state == branch.BranchMetadata?.OutgoingPullRequest?.PullRequest?.State)
                && branch.BranchMetadata?.OutgoingPullRequest?.PullRequest?.UpdatedDate < DateTimeOffset.UtcNow.Date.AddDays(-daysOlderThanToday)
                && branch.BranchMetadata?.AheadBehind?.Ahead == 0);

            Assert.NotNull(branchesToDelete);
        }

        [Theory]
        [InlineData("Tools", "Test")]
        public async Task BrowseProjectRepositoryAsync(string projectKey, string repositorySlug)
        {
            var result = await _client.BrowseProjectRepositoryAsync(projectKey, repositorySlug, null).ConfigureAwait(false);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("Tools", "Test", "hello.txt")]
        public async Task BrowseProjectRepositoryPathAsync(string projectKey, string repositorySlug, string path)
        {
            var result = await _client.BrowseProjectRepositoryPathAsync(projectKey, repositorySlug, path, null).ConfigureAwait(false);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("Tools", "Test")]
        public async Task GetRepositoryFilesAsync(string projectKey, string repositorySlug)
        {
            var results = await _client.GetRepositoryFilesAsync(projectKey, repositorySlug);
            Assert.True(results.Any());
        }

        [Theory]
        [InlineData("Tools", "Test")]
        public async Task GetProjectRepositoryLastModifiedAsync(string projectKey, string repositorySlug)
        {
            var result = await _client.GetProjectRepositoryLastModifiedAsync(projectKey, repositorySlug, "4bbc14ed0090bba975323023af235e465c527312");
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("Tools", "Test", PullRequestStates.All)]
        [InlineData("Tools", "Test", PullRequestStates.Merged)]
        public async Task GetPullRequestsAsync(string projectKey, string repositorySlug, PullRequestStates state)
        {
            var results = await _client.GetPullRequestsAsync(projectKey, repositorySlug, state: state, maxPages: 1).ConfigureAwait(false);
            Assert.True(results.Any());
        }

        [Theory]
        [InlineData("Tools", "Test", PullRequestStates.All)]
        public async Task GetPullRequestAsync(string projectKey, string repositorySlug, PullRequestStates state)
        {
            var results = await _client.GetPullRequestsAsync(projectKey, repositorySlug, state: state, maxPages: 1).ConfigureAwait(false);
            var list = results.ToList();
            Assert.True(list.Any());
            int id = list.First().Id;

            var result = await _client.GetPullRequestAsync(projectKey, repositorySlug, id).ConfigureAwait(false);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("Tools", "Test")]
        public async Task CreateAndDeletePullRequestAsync(string projectKey, string repositorySlug)
        {
            var result = await _client.CreatePullRequestAsync(projectKey, repositorySlug, new PullRequestInfo
            {
                Title = "Test Pull Request",
                Description = "This is a test pull request",
                State = PullRequestStates.Open,
                Open = true,
                Closed = false,
                FromRef = new FromToRef
                {
                    Id = "refs/heads/feature-test",
                    Repository = new RepositoryRef
                    {
                        Name = null,
                        Slug = repositorySlug,
                        Project = new ProjectRef { Key = projectKey }
                    }
                },
                ToRef = new FromToRef
                {
                    Id = "refs/heads/master",
                    Repository = new RepositoryRef
                    {
                        Name = null,
                        Slug = repositorySlug,
                        Project = new ProjectRef { Key = projectKey }
                    }
                },
                Locked = false
            }).ConfigureAwait(false);

            int id = result.Id;
            var pullRequest = await _client.GetPullRequestAsync(projectKey, repositorySlug, id).ConfigureAwait(false);
            Assert.NotNull(pullRequest);

            await _client.DeletePullRequestAsync(projectKey, repositorySlug, pullRequest.Id, new VersionInfo { Version = -1 }).ConfigureAwait(false);
        }

        [Theory]
        [InlineData("Tools", "Test", 1)]
        public async Task GetPullRequestCommentsAsync(string projectKey, string repositorySlug, long pullRequestId)
        {
            var results = await _client.GetPullRequestCommentsAsync(projectKey, repositorySlug, pullRequestId, "/").ConfigureAwait(false);
            Assert.NotEmpty(results);
        }

        [Theory]
        [InlineData("Tools", "Test", 1, 1)]
        public async Task GetPullRequestCommentAsync(string projectKey, string repositorySlug, long pullRequestId, long commentId)
        {
            var result = await _client.GetPullRequestCommentAsync(projectKey, repositorySlug, pullRequestId, commentId).ConfigureAwait(false);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("Tools", "Test", 1)]
        public async Task GetPullRequestCommitsAsync(string projectKey, string repositorySlug, long pullRequestId)
        {
            var results = await _client.GetPullRequestCommitsAsync(projectKey, repositorySlug, pullRequestId).ConfigureAwait(false);
            Assert.NotEmpty(results);
        }

        [Theory]
        [InlineData("Tools", "Test", 1)]
        public async Task GetPullRequestDiffAsync(string projectKey, string repositorySlug, long pullRequestId)
        {
            var result = await _client.GetPullRequestDiffAsync(projectKey, repositorySlug, pullRequestId).ConfigureAwait(false);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("Tools", "Test", 1)]
        public async Task GetPullRequestDiffPathAsync(string projectKey, string repositorySlug, long pullRequestId)
        {
            var result = await _client.GetPullRequestDiffPathAsync(projectKey, repositorySlug, pullRequestId, "hello.txt").ConfigureAwait(false);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("Tools", "Test", 1)]
        public async Task GetPullRequestParticipantsAsync(string projectKey, string repositorySlug, long pullRequestId)
        {
            var results = await _client.GetPullRequestParticipantsAsync(projectKey, repositorySlug, pullRequestId).ConfigureAwait(false);
            Assert.NotEmpty(results);
        }

        [Theory]
        [InlineData("Tools", "Test", 1)]
        public async Task GetPullRequestTasksAsync(string projectKey, string repositorySlug, long pullRequestId)
        {
            var results = await _client.GetPullRequestTasksAsync(projectKey, repositorySlug, pullRequestId).ConfigureAwait(false);
            Assert.NotEmpty(results);
        }

        [Theory]
        [InlineData("Tools", "Test", 1)]
        public async Task GetPullRequestTaskCountAsync(string projectKey, string repositorySlug, long pullRequestId)
        {
            var result = await _client.GetPullRequestTaskCountAsync(projectKey, repositorySlug, pullRequestId).ConfigureAwait(false);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("Tools", "Test")]
        public async Task GetProjectRepositoryPullRequestSettingsAsync(string projectKey, string repositorySlug)
        {
            var result = await _client.GetProjectRepositoryPullRequestSettingsAsync(projectKey, repositorySlug);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("Tools", "Test")]
        public async Task GetProjectRepositoryHooksSettingsAsync(string projectKey, string repositorySlug)
        {
            var results = await _client.GetProjectRepositoryHooksSettingsAsync(projectKey, repositorySlug);
            Assert.NotEmpty(results);
        }

        [Theory]
        [InlineData("Tools", "Test", "com.atlassian.bitbucket.server.bitbucket-bundled-hooks:all-approvers-merge-check")]
        public async Task GetProjectRepositoryHookSettingsAsync(string projectKey, string repositorySlug, string hookKey)
        {
            var result = await _client.GetProjectRepositoryHookSettingsAsync(projectKey, repositorySlug, hookKey);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("Tools", "Test", "com.ngs.stash.externalhooks.external-hooks:external-post-receive-hook")]
        public async Task GetProjectRepositoryHookAllSettingsAsync(string projectKey, string repositorySlug, string hookKey)
        {
            // ReSharper disable once UnusedVariable
            var result = await _client.GetProjectRepositoryHookAllSettingsAsync(projectKey, repositorySlug, hookKey);
        }

        [Theory]
        [InlineData("Tools")]
        public async Task GetProjectPullRequestsMergeStrategiesAsync(string projectKey)
        {
            var result = await _client.GetProjectPullRequestsMergeStrategiesAsync(projectKey, "git");
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("Tools", "Test")]
        public async Task GetProjectRepositoryTagsAsync(string projectKey, string repositorySlug)
        {
            var results = await _client.GetProjectRepositoryTagsAsync(projectKey, repositorySlug, "v1", BranchOrderBy.Alphabetical).ConfigureAwait(false);
            Assert.NotEmpty(results);
        }

        [Theory]
        [InlineData("Tools", "Test")]
        public async Task GetProjectRepositoryTagAsync(string projectKey, string repositorySlug)
        {
            var result = await _client.GetProjectRepositoryTagAsync(projectKey, repositorySlug, "v1").ConfigureAwait(false);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("Tools", "Test")]
        public async Task GetProjectRepositoryWebHooksAsync(string projectKey, string repositorySlug)
        {
            var results = await _client.GetProjectRepositoryWebHooksAsync(projectKey, repositorySlug).ConfigureAwait(false);
            Assert.NotNull(results);
        }

        [Theory]
        [InlineData("Tools", "Test", "1")]
        public async Task GetProjectRepositoryWebHookAsync(string projectKey, string repositorySlug, string webHookId)
        {
            var result = await _client.GetProjectRepositoryWebHookAsync(projectKey, repositorySlug, webHookId).ConfigureAwait(false);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("Tools", "Test", "1")]
        public async Task GetProjectRepositoryWebHookLatestAsync(string projectKey, string repositorySlug, string webHookId)
        {
            // ReSharper disable once UnusedVariable
            var result = await _client.GetProjectRepositoryWebHookLatestAsync(projectKey, repositorySlug, webHookId, "pr:reviewer:unapproved").ConfigureAwait(false);
        }

        [Theory]
        [InlineData("Tools", "Test", "1")]
        public async Task GetProjectRepositoryWebHookStatisticsAsync(string projectKey, string repositorySlug, string webHookId)
        {
            var result = await _client.GetProjectRepositoryWebHookStatisticsAsync(projectKey, repositorySlug, webHookId).ConfigureAwait(false);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("Tools", "Test", "1")]
        public async Task GetProjectRepositoryWebHookStatisticsSummaryAsync(string projectKey, string repositorySlug, string webHookId)
        {
            var result = await _client.GetProjectRepositoryWebHookStatisticsSummaryAsync(projectKey, repositorySlug, webHookId).ConfigureAwait(false);
            Assert.NotNull(result);
        }
    }
}
