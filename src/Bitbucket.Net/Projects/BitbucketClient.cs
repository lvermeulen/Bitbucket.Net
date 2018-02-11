using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Bitbucket.Net.Common;
using Bitbucket.Net.Models.Common;
using Bitbucket.Net.Models.Projects;
using Flurl.Http;
using Newtonsoft.Json;

namespace Bitbucket.Net
{
    public partial class BitbucketClient
    {
        private IFlurlRequest GetProjectsUrl() => GetBaseUrl()
            .AppendPathSegment("/projects")
            .WithBasicAuth(_userName, _password);

        private IFlurlRequest GetProjectsUrl(string path) => GetProjectsUrl()
            .AppendPathSegment(path);

        private IFlurlRequest GetProjectsReposUrl(string projectKey, string repositorySlug) => GetProjectsUrl($"/{projectKey}/repos/{repositorySlug}");

        private IFlurlRequest GetProjectsReposUrl(string projectKey, string repositorySlug, string path) => GetProjectsReposUrl(projectKey, repositorySlug)
            .AppendPathSegment(path);

        public async Task<IEnumerable<Project>> GetProjectsAsync(
            int? maxPages = null,
            int? limit = null,
            int? start = null,
            string name = null,
            Permissions? permission = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start,
                ["name"] = name,
                ["permission"] = BitbucketHelpers.PermissionToString(permission)
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                await GetProjectsUrl()
                    .SetQueryParams(qpv)
                    .GetJsonAsync<BitbucketResult<Project>>()
                    .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<Project> CreateProjectAsync(ProjectDefinition projectDefinition)
        {
            var response = await GetProjectsUrl()
                .ConfigureRequest(settings => settings.JsonSerializer = s_serializer)
                .PostJsonAsync(projectDefinition)
                .ConfigureAwait(false);

            return await HandleResponseAsync<Project>(response).ConfigureAwait(false);
        }

        public async Task<bool> DeleteProjectAsync(string projectKey)
        {
            var response = await GetProjectsUrl($"/{projectKey}")
                .DeleteAsync()
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<Project> UpdateProjectAsync(string projectKey, ProjectDefinition projectDefinition)
        {
            var response = await GetProjectsUrl($"/{projectKey}")
                .ConfigureRequest(settings => settings.JsonSerializer = s_serializer)
                .PutJsonAsync(projectDefinition)
                .ConfigureAwait(false);

            return await HandleResponseAsync<Project>(response).ConfigureAwait(false);
        }

        public async Task<Project> GetProjectAsync(string projectKey)
        {
            var response = await GetProjectsUrl($"/{projectKey}")
                .ConfigureRequest(settings => settings.JsonSerializer = s_serializer)
                .GetJsonAsync()
                .ConfigureAwait(false);

            return await HandleResponseAsync<Project>(response).ConfigureAwait(false);
        }

        public async Task<IEnumerable<UserPermission>> GetProjectUserPermissionsAsync(string projectKey, string filter = null,
            int? maxPages = null,
            int? limit = null,
            int? start = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start,
                ["filter"] = filter
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                await GetProjectsUrl($"/{projectKey}/permissions/users")
                    .SetQueryParams(qpv)
                    .GetJsonAsync<BitbucketResult<UserPermission>>()
                    .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<bool> DeleteProjectUserPermissionsAsync(string projectKey, string userName)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["name"] = userName
            };

            var response = await GetProjectsUrl($"/{projectKey}/permissions/users")
                .SetQueryParams(queryParamValues)
                .DeleteAsync()
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<bool> UpdateProjectUserPermissionsAsync(string projectKey, string userName, Permissions permission)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["name"] = userName,
                ["permission"] = BitbucketHelpers.PermissionToString(permission)
            };

            var response = await GetProjectsUrl($"/{projectKey}/permissions/users")
                .SetQueryParams(queryParamValues)
                .PutAsync(new StringContent(""))
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<IEnumerable<LicensedUser>> GetProjectUserPermissionsNoneAsync(string projectKey, string filter = null,
            int? maxPages = null,
            int? limit = null,
            int? start = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start,
                ["filter"] = filter
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                    await GetProjectsUrl($"/{projectKey}/permissions/users/none")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<BitbucketResult<LicensedUser>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<GroupPermission>> GetProjectGroupPermissionsAsync(string projectKey, string filter = null,
            int? maxPages = null,
            int? limit = null,
            int? start = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start,
                ["filter"] = filter
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                await GetProjectsUrl($"/{projectKey}/permissions/groups")
                    .SetQueryParams(qpv)
                    .GetJsonAsync<BitbucketResult<GroupPermission>>()
                    .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<bool> DeleteProjectGroupPermissionsAsync(string projectKey, string groupName)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["name"] = groupName
            };

            var response = await GetProjectsUrl($"/{projectKey}/permissions/groups")
                .SetQueryParams(queryParamValues)
                .DeleteAsync()
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<bool> UpdateProjectGroupPermissionsAsync(string projectKey, string groupName, Permissions permission)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["name"] = groupName,
                ["permission"] = BitbucketHelpers.PermissionToString(permission)
            };

            var response = await GetProjectsUrl($"/{projectKey}/permissions/groups")
                .SetQueryParams(queryParamValues)
                .PutAsync(new StringContent(""))
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<IEnumerable<LicensedUser>> GetProjectGroupPermissionsNoneAsync(string projectKey, string filter = null,
            int? maxPages = null,
            int? limit = null,
            int? start = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start,
                ["filter"] = filter
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                    await GetProjectsUrl($"/{projectKey}/permissions/groups/none")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<BitbucketResult<LicensedUser>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<bool> IsProjectDefaultPermissionAsync(string projectKey, Permissions permission)
        {
            var response = await GetProjectsUrl($"/{projectKey}/permissions/{BitbucketHelpers.PermissionToString(permission)}/all")
                .GetAsync()
                .ConfigureAwait(false);

            return await HandleResponseAsync<bool>(response, s => 
                    BitbucketHelpers.StringToBool(JsonConvert.DeserializeObject<dynamic>(s).permitted.ToString()))
                .ConfigureAwait(false);
        }

        private async Task<bool> SetProjectDefaultPermissionAsync(string projectKey, Permissions permission, bool allow)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["allow"] = BitbucketHelpers.BoolToString(allow)
            };

            var response = await GetProjectsUrl($"/{projectKey}/permissions/{BitbucketHelpers.PermissionToString(permission)}/all")
                .SetQueryParams(queryParamValues)
                .PostAsync(new StringContent(""))
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<bool> GrantProjectPermissionToAllAsync(string projectKey, Permissions permission)
        {
            return await SetProjectDefaultPermissionAsync(projectKey, permission, true);
        }

        public async Task<bool> RevokeProjectPermissionFromAllAsync(string projectKey, Permissions permission)
        {
            return await SetProjectDefaultPermissionAsync(projectKey, permission, false);
        }

        public async Task<IEnumerable<Repository>> GetRepositoriesAsync(string projectKey,
            int? maxPages = null,
            int? limit = null,
            int? start = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                await GetProjectsUrl($"/{projectKey}")
                    .SetQueryParams(qpv)
                    .GetJsonAsync<BitbucketResult<Repository>>()
                    .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<GroupPermission>> GetRepositoryGroupPermissionsAsync(string projectKey, string repositorySlug,
            string filter = null,
            int? maxPages = null,
            int? limit = null,
            int? start = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["filter"] = filter,
                ["limit"] = limit,
                ["start"] = start
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                    await GetProjectsReposUrl(projectKey, repositorySlug, "/permissions/groups")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<BitbucketResult<GroupPermission>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<UserPermission>> GetRepositoryUserPermissionsAsync(string projectKey, string repositorySlug,
            string filter = null,
            int? maxPages = null,
            int? limit = null,
            int? start = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["filter"] = filter,
                ["limit"] = limit,
                ["start"] = start
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                    await GetProjectsReposUrl(projectKey, repositorySlug, "/permissions/users")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<BitbucketResult<UserPermission>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<Branch>> GetBranchesAsync(string projectKey, string repositorySlug,
            int? maxPages = null,
            int? limit = null,
            int? start = null,
            string baseBranchOrTag = null,
            bool? details = null,
            string filterText = null,
            BranchOrderBy? orderBy = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start,
                ["base"] = baseBranchOrTag,
                ["details"] = details.HasValue ? BitbucketHelpers.BoolToString(details.Value) : null,
                ["filterText"] = filterText,
                ["orderBy"] = orderBy.HasValue ? BitbucketHelpers.BranchOrderByToString(orderBy.Value) : null
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                await GetProjectsReposUrl(projectKey, repositorySlug, "/branches")
                    .SetQueryParams(qpv)
                    .GetJsonAsync<BitbucketResult<Branch>>()
                    .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<Branch> CreateBranchAsync(string projectKey, string repositorySlug, BranchInfo branchInfo)
        {
            var response = await GetProjectsReposUrl(projectKey, repositorySlug, "/branches")
                .ConfigureRequest(settings => settings.JsonSerializer = s_serializer)
                .PostJsonAsync(branchInfo)
                .ConfigureAwait(false);

            return await HandleResponseAsync<Branch>(response).ConfigureAwait(false);
        }

        public async Task<Branch> GetDefaultBranchAsync(string projectKey, string repositorySlug)
        {
            return await GetProjectsReposUrl(projectKey, repositorySlug, "/branches/default")
                .GetJsonAsync<Branch>()
                .ConfigureAwait(false);
        }

        public async Task<bool> SetDefaultBranchAsync(string projectKey, string repositorySlug, BranchRef branchRef)
        {
            var response = await GetProjectsReposUrl(projectKey, repositorySlug, "/branches")
                .ConfigureRequest(settings => settings.JsonSerializer = s_serializer)
                .PutJsonAsync(branchRef)
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Change>> GetChangesAsync(string projectKey, string repositorySlug, string until, string since = null,
            int? maxPages = null,
            int? limit = null,
            int? start = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start,
                ["since"] = since,
                ["until"] = until
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                    await GetProjectsReposUrl(projectKey, repositorySlug, "/changes")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<BitbucketResult<Change>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<Commit>> GetCommitsAsync(string projectKey, string repositorySlug, 
            string until, 
            bool followRenames = false, 
            bool ignoreMissing = false, 
            MergeCommits merges = MergeCommits.Exclude,
            string path = null, 
            string since = null, 
            bool withCounts = false,
            int? maxPages = null,
            int? limit = null,
            int? start = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start,
                ["followRenames"] = BitbucketHelpers.BoolToString(followRenames),
                ["ignoreMissing"] = BitbucketHelpers.BoolToString(ignoreMissing),
                ["merges"] = BitbucketHelpers.MergeCommitsToString(merges),
                ["path"] = path,
                ["since"] = since,
                ["until"] = until,
                ["withCounts"] = BitbucketHelpers.BoolToString(withCounts),
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                    await GetProjectsReposUrl(projectKey, repositorySlug, "/commits")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<BitbucketResult<Commit>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<Commit> GetCommitAsync(string projectKey, string repositorySlug, string commitId, string path = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["path"] = path,
            };

            return await GetProjectsReposUrl(projectKey, repositorySlug, $"/commits/{commitId}")
                .SetQueryParams(queryParamValues)
                .GetJsonAsync<Commit>()
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<Change>> GetCommitChangesAsync(string projectKey, string repositorySlug, string commitId,
            string since = null,
            bool withComments = true,
            int? maxPages = null,
            int? limit = null,
            int? start = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start,
                ["since"] = since,
                ["withComments"] = BitbucketHelpers.BoolToString(withComments),
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                    await GetProjectsReposUrl(projectKey, repositorySlug, $"/commits/{commitId}/changes")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<BitbucketResult<Change>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<Comment>> GetCommitCommentsAsync(string projectKey, string repositorySlug, string commitId,
            string path,
            string since = null,
            int? maxPages = null,
            int? limit = null,
            int? start = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start,
                ["path"] = path,
                ["since"] = since
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                    await GetProjectsReposUrl(projectKey, repositorySlug, $"/commits/{commitId}/comments")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<BitbucketResult<Comment>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<CommentRef> CreateCommitCommentAsync(string projectKey, string repositorySlug, string commitId, 
            CommentInfo commentInfo, string since = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["since"] = since
            };

            var response = await GetProjectsReposUrl(projectKey, repositorySlug, $"/commits/{commitId}/comments")
                .ConfigureRequest(settings => settings.JsonSerializer = s_serializer)
                .SetQueryParams(queryParamValues)
                .PostJsonAsync(commentInfo)
                .ConfigureAwait(false);

            return await HandleResponseAsync<CommentRef>(response).ConfigureAwait(false);
        }

        public async Task<CommentRef> GetCommitCommentAsync(string projectKey, string repositorySlug, string commitId, long commentId)
        {
            return await GetProjectsReposUrl(projectKey, repositorySlug, $"/commits/{commitId}/comments/{commentId}")
                .GetJsonAsync<CommentRef>()
                .ConfigureAwait(false);
        }

        public async Task<CommentRef> UpdateCommitCommentAsync(string projectKey, string repositorySlug, string commitId, long commentId, 
            CommentText commentText)
        {
            var response = await GetProjectsReposUrl(projectKey, repositorySlug, $"/commits/{commitId}/comments/{commentId}")
                .ConfigureRequest(settings => settings.JsonSerializer = s_serializer)
                .PutJsonAsync(commentText)
                .ConfigureAwait(false);

            return await HandleResponseAsync<CommentRef>(response).ConfigureAwait(false);
        }

        public async Task<bool> DeleteCommitCommentAsync(string projectKey, string repositorySlug, string commitId, long commentId,
            int version = -1)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["version"] = version
            };

            var response = await GetProjectsReposUrl(projectKey, repositorySlug, $"/commits/{commitId}/comments/{commentId}")
                .ConfigureRequest(settings => settings.JsonSerializer = s_serializer)
                .SetQueryParams(queryParamValues)
                .DeleteAsync()
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<Differences> GetCommitDiffAsync(string projectKey, string repositorySlug, string commitId,
            bool autoSrcPath = false,
            int contextLines = -1,
            string since = null,
            string srcPath = null,
            string whitespace = "ignore-all",
            bool withComments = true)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["autoSrcPath"] = BitbucketHelpers.BoolToString(autoSrcPath),
                ["contextLines"] = contextLines,
                ["since"] = since,
                ["srcPath"] = srcPath,
                ["whitespace"] = whitespace,
                ["withComments"] = BitbucketHelpers.BoolToString(withComments),
            };

            return await GetProjectsReposUrl(projectKey, repositorySlug, $"/commits/{commitId}/diff")
                .SetQueryParams(queryParamValues)
                .GetJsonAsync<Differences>()
                .ConfigureAwait(false);
        }

        public async Task<bool> CreateCommitWatchAsync(string projectKey, string repositorySlug, string commitId)
        {
            var response = await GetProjectsReposUrl(projectKey, repositorySlug, $"/commits/{commitId}/watch")
                .PostAsync(new StringContent(""))
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<bool> DeleteCommitWatchAsync(string projectKey, string repositorySlug, string commitId)
        {
            var response = await GetProjectsReposUrl(projectKey, repositorySlug, $"/commits/{commitId}/watch")
                .DeleteAsync()
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Change>> GetRepositoryCompareChangesAsync(string projectKey, string repositorySlug, string from, string to, 
            string fromRepo = null,
            int? maxPages = null,
            int? limit = null,
            int? start = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start,
                ["from"] = from,
                ["to"] = to,
                ["fromRepo"] = fromRepo
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                    await GetProjectsReposUrl(projectKey, repositorySlug, "/compare/changes")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<BitbucketResult<Change>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<Differences> GetRepositoryCompareDiffAsync(string projectKey, string repositorySlug, string from, string to,
            string fromRepo = null,
            string srcPath = null,
            int contextLines = -1,
            string whitespace = "ignore-all")
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["from"] = from,
                ["to"] = to,
                ["fromRepo"] = fromRepo,
                ["srcPath"] = srcPath,
                ["contextLines"] = contextLines,
                ["whitespace"] = whitespace,
            };

            return await GetProjectsReposUrl(projectKey, repositorySlug, "/compare/diff")
                .SetQueryParams(queryParamValues)
                .GetJsonAsync<Differences>()
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<Commit>> GetRepositoryCompareCommitsAsync(string projectKey, string repositorySlug, string from, string to,
            string fromRepo = null,
            int? maxPages = null,
            int? limit = null,
            int? start = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start,
                ["from"] = from,
                ["to"] = to,
                ["fromRepo"] = fromRepo
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                await GetProjectsReposUrl(projectKey, repositorySlug, "/compare/commits")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<BitbucketResult<Commit>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<Differences> GetRepositoryDiffAsync(string projectKey, string repositorySlug, string until,
            int contextLines = -1, 
            string since = null, 
            string srcPath = null,
            string whitespace = "ignore-all")
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["contextLines"] = contextLines,
                ["since"] = since,
                ["srcPath"] = srcPath,
                ["until"] = until,
                ["whitespace"] = whitespace
            };

            return await GetProjectsReposUrl(projectKey, repositorySlug, "/diff")
                .SetQueryParams(queryParamValues)
                .GetJsonAsync<Differences>()
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<string>> GetRepositoryFilesAsync(string projectKey, string repositorySlug, string at = null,
            int? maxPages = null,
            int? limit = null,
            int? start = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start,
                ["at"] = at
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                    await GetProjectsReposUrl(projectKey, repositorySlug, "/files")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<BitbucketResult<string>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<Identity>> GetRepositoryParticipantsAsync(string projectKey, string repositorySlug, 
            PullRequestDirections direction = PullRequestDirections.Incoming,
            string filter = null,
            Roles? role = null,
            int? maxPages = null,
            int? limit = null,
            int? start = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start,
                ["direction"] = BitbucketHelpers.PullRequestDirectionToString(direction),
                ["filter"] = filter,
                ["role"] = BitbucketHelpers.RoleToString(role)
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                    await GetProjectsReposUrl(projectKey, repositorySlug, "/participants")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<BitbucketResult<Identity>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<PullRequest>> GetPullRequestsAsync(string projectKey, string repositorySlug,
            int? maxPages = null,
            int? limit = null,
            int? start = null,
            PullRequestDirections direction = PullRequestDirections.Incoming,
            string branchId = null,
            PullRequestStates state = PullRequestStates.Open,
            PullRequestOrders order = PullRequestOrders.Newest,
            bool withAttributes = true,
            bool withProperties = true)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start,
                ["direction"] = BitbucketHelpers.PullRequestDirectionToString(direction),
                ["at"] = branchId,
                ["state"] = BitbucketHelpers.PullRequestStateToString(state),
                ["order"] = BitbucketHelpers.PullRequestOrderToString(order),
                ["withAttributes"] = BitbucketHelpers.BoolToString(withAttributes),
                ["withProperties"] = BitbucketHelpers.BoolToString(withProperties),
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                await GetProjectsReposUrl(projectKey, repositorySlug, "/pull-requests")
                    .SetQueryParams(qpv)
                    .GetJsonAsync<BitbucketResult<PullRequest>>()
                    .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<PullRequest> CreatePullRequestAsync(string projectKey, string repositorySlug, PullRequestInfo pullRequestInfo)
        {
            var response = await GetProjectsReposUrl(projectKey, repositorySlug, "/pull-requests")
                .ConfigureRequest(settings => settings.JsonSerializer = s_serializer)
                .PostJsonAsync(pullRequestInfo)
                .ConfigureAwait(false);

            return await HandleResponseAsync<PullRequest>(response).ConfigureAwait(false);
        }

        public async Task<PullRequest> GetPullRequestAsync(string projectKey, string repositorySlug, long pullRequestId)
        {
            return await GetProjectsReposUrl(projectKey, repositorySlug, $"/pull-requests/{pullRequestId}")
                .GetJsonAsync<PullRequest>()
                .ConfigureAwait(false);
        }

        public async Task<PullRequest> UpdatePullRequestAsync(string projectKey, string repositorySlug, long pullRequestId, PullRequestUpdate pullRequestUpdate)
        {
            var response = await GetProjectsReposUrl(projectKey, repositorySlug, $"/pull-requests/{pullRequestId}")
                .ConfigureRequest(settings => settings.JsonSerializer = s_serializer)
                .PutJsonAsync(pullRequestUpdate)
                .ConfigureAwait(false);

            return await HandleResponseAsync<PullRequest>(response).ConfigureAwait(false);
        }

        public async Task<bool> DeletePullRequestAsync(string projectKey, string repositorySlug, long pullRequestId, VersionInfo versionInfo)
        {
            var response = await GetProjectsReposUrl(projectKey, repositorySlug, $"/pull-requests/{pullRequestId}")
                .ConfigureRequest(settings => settings.JsonSerializer = s_serializer)
                .SendJsonAsync(HttpMethod.Delete, versionInfo)
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<IEnumerable<PullRequestActivity>> GetPullRequestActivitiesAsync(string projectKey, string repositorySlug, long pullRequestId,
            long? fromId = null,
            PullRequestFromTypes? fromType = null,
            int? maxPages = null,
            int? limit = null,
            int? start = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start,
                ["fromId"] = fromId,
                ["fromType"] = BitbucketHelpers.PullRequestFromTypeToString(fromType)
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                    await GetProjectsReposUrl(projectKey, repositorySlug, $"/pull-requests/{pullRequestId}/activities")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<BitbucketResult<PullRequestActivity>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<bool> DeclinePullRequestAsync(string projectKey, string repositorySlug, long pullRequestId, int version = -1)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["version"] = version
            };

            var response = await GetProjectsReposUrl(projectKey, repositorySlug, $"/pull-requests/{pullRequestId}/decline")
                .SetQueryParams(queryParamValues)
                .DeleteAsync()
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<PullRequestMergeState> GetPullRequestMergeStateAsync(string projectKey, string repositorySlug, long pullRequestId, int version = -1)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["version"] = version
            };

            return await GetProjectsReposUrl(projectKey, repositorySlug, $"/pull-requests/{pullRequestId}/merge")
                .SetQueryParams(queryParamValues)
                .GetJsonAsync<PullRequestMergeState>()
                .ConfigureAwait(false);
        }

        public async Task<PullRequest> MergePullRequestAsync(string projectKey, string repositorySlug, long pullRequestId, int version = -1)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["version"] = version
            };

            var response = await GetProjectsReposUrl(projectKey, repositorySlug, $"/pull-requests/{pullRequestId}/merge")
                .SetQueryParams(queryParamValues)
                .PostJsonAsync(new StringContent(""))
                .ConfigureAwait(false);

            return await HandleResponseAsync<PullRequest>(response).ConfigureAwait(false);
        }

        public async Task<PullRequest> ReopenPullRequestAsync(string projectKey, string repositorySlug, long pullRequestId, int version = -1)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["version"] = version
            };

            var response = await GetProjectsReposUrl(projectKey, repositorySlug, $"/pull-requests/{pullRequestId}/reopen")
                .SetQueryParams(queryParamValues)
                .PostJsonAsync(new StringContent(""))
                .ConfigureAwait(false);

            return await HandleResponseAsync<PullRequest>(response).ConfigureAwait(false);
        }

        public async Task<Reviewer> ApprovePullRequestAsync(string projectKey, string repositorySlug, long pullRequestId)
        {
            var response = await GetProjectsReposUrl(projectKey, repositorySlug, $"/pull-requests/{pullRequestId}/approve")
                .PostJsonAsync(new StringContent(""))
                .ConfigureAwait(false);

            return await HandleResponseAsync<Reviewer>(response).ConfigureAwait(false);
        }

        public async Task<Reviewer> DeletePullRequestApprovalAsync(string projectKey, string repositorySlug, long pullRequestId)
        {
            var response = await GetProjectsReposUrl(projectKey, repositorySlug, $"/pull-requests/{pullRequestId}/approve")
                .DeleteAsync()
                .ConfigureAwait(false);

            return await HandleResponseAsync<Reviewer>(response).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Change>> GetPullRequestChangesAsync(string projectKey, string repositorySlug, long pullRequestId,
            ChangeScopes changeScope = ChangeScopes.All,
            string sinceId = null,
            string untilId = null,
            bool withComments = true,
            int? maxPages = null,
            int? limit = null,
            int? start = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start,
                ["changeScope"] = BitbucketHelpers.ChangeScopeToString(changeScope),
                ["sinceId"] = sinceId,
                ["untilId"] = untilId,
                ["withComments"] = BitbucketHelpers.BoolToString(withComments)
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                    await GetProjectsReposUrl(projectKey, repositorySlug, $"/pull-requests/{pullRequestId}/changes")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<BitbucketResult<Change>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }
    }
}
