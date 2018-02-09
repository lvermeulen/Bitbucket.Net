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
                    await GetProjectsUrl($"/{projectKey}/repos/{repositorySlug}/permissions/groups")
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
                    await GetProjectsUrl($"/{projectKey}/repos/{repositorySlug}/permissions/users")
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
                await GetProjectsUrl($"/{projectKey}/repos/{repositorySlug}/branches")
                    .SetQueryParams(qpv)
                    .GetJsonAsync<BitbucketResult<Branch>>()
                    .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<Branch> CreateBranchAsync(string projectKey, string repositorySlug, BranchInfo branchInfo)
        {
            var response = await GetProjectsUrl($"/{projectKey}/repos/{repositorySlug}/branches")
                .ConfigureRequest(settings => settings.JsonSerializer = s_serializer)
                .PostJsonAsync(branchInfo)
                .ConfigureAwait(false);

            return await HandleResponseAsync<Branch>(response).ConfigureAwait(false);
        }

        public async Task<Branch> GetDefaultBranchAsync(string projectKey, string repositorySlug)
        {
            return await GetProjectsUrl($"/{projectKey}/repos/{repositorySlug}/branches/default")
                .GetJsonAsync<Branch>()
                .ConfigureAwait(false);
        }

        public async Task<bool> SetDefaultBranchAsync(string projectKey, string repositorySlug, BranchRef branchRef)
        {
            var response = await GetProjectsUrl($"/{projectKey}/repos/{repositorySlug}/branches")
                .ConfigureRequest(settings => settings.JsonSerializer = s_serializer)
                .PutJsonAsync(branchRef)
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<IEnumerable<PullRequest>> GetPullRequestsAsync(string projectKey, string repositorySlug,
            int? maxPages = null,
            int? limit = null,
            int? start = null,
            PullRequestDirection direction = PullRequestDirection.Incoming,
            string branchId = null,
            PullRequestState state = PullRequestState.Open,
            PullRequestOrder order = PullRequestOrder.Newest,
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
                await GetProjectsUrl($"/{projectKey}/repos/{repositorySlug}/pull-requests")
                    .SetQueryParams(qpv)
                    .GetJsonAsync<BitbucketResult<PullRequest>>()
                    .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<PullRequest> GetPullRequestAsync(string projectKey, string repositorySlug, int id)
        {
            return await GetProjectsUrl($"/{projectKey}/repos/{repositorySlug}/pull-requests/{id}")
                .GetJsonAsync<PullRequest>()
                .ConfigureAwait(false);
        }

        public async Task<PullRequest> CreatePullRequestAsync(string projectKey, string repositorySlug, PullRequestInfo pullRequestInfo)
        {
            var response = await GetProjectsUrl($"/{projectKey}/repos/{repositorySlug}/pull-requests")
                .ConfigureRequest(settings => settings.JsonSerializer = s_serializer)
                .PostJsonAsync(pullRequestInfo)
                .ConfigureAwait(false);

            return await HandleResponseAsync<PullRequest>(response).ConfigureAwait(false);
        }

        public async Task<bool> DeletePullRequest(string projectKey, string repositorySlug, PullRequest pullRequest)
        {
            var response = await GetProjectsUrl($"/{projectKey}/repos/{repositorySlug}/pull-requests/{pullRequest.Id}")
                .ConfigureRequest(settings => settings.JsonSerializer = s_serializer)
                .SendJsonAsync(HttpMethod.Delete, new VersionInfo { Version = pullRequest.Version })
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }
    }
}
