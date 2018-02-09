﻿using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Bitbucket.Net.Common;
using Bitbucket.Net.Models.Common;
using Bitbucket.Net.Models.Projects;
using Flurl.Http;
using Flurl.Http.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Bitbucket.Net
{
    public partial class BitbucketClient
    {
        private IFlurlClient GetProjectsUrl(string path = null) => GetBaseUrl()
            .AppendPathSegment("/projects")
            .AppendPathSegment(path)
            .WithBasicAuth(_userName, _password);

        public async Task<IEnumerable<Project>> GetProjectsAsync(
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
                await GetProjectsUrl()
                    .GetJsonAsync<BitbucketResult<Project>>()
                    .ConfigureAwait(false))
                .ConfigureAwait(false);
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
                    .GetJsonAsync<BitbucketResult<Branch>>()
                    .ConfigureAwait(false))
                .ConfigureAwait(false);
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
                .ConfigureClient(settings => settings.JsonSerializer = new NewtonsoftJsonSerializer(new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }))
                .PostJsonAsync(pullRequestInfo)
                .ConfigureAwait(false);

            return await HandleResponseAsync<PullRequest>(response).ConfigureAwait(false);
        }

        public async Task<bool> DeletePullRequest(string projectKey, string repositorySlug, PullRequest pullRequest)
        {
            var response = await GetProjectsUrl($"/{projectKey}/repos/{repositorySlug}/pull-requests/{pullRequest.Id}")
                .ConfigureClient(settings => settings.JsonSerializer = new NewtonsoftJsonSerializer(new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }))
                .SendJsonAsync(HttpMethod.Delete, new VersionInfo { Version = pullRequest.Version })
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }
    }
}