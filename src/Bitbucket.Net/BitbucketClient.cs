using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bitbucket.Net.Common;
using Bitbucket.Net.Models;
using Flurl;
using Flurl.Http;

namespace Bitbucket.Net
{
    public class BitbucketClient
    {
        private readonly Url _url;
        private readonly string _userName;
        private readonly string _password;

        public BitbucketClient(string url, string userName, string password)
        {
            _url = url;
            _userName = userName;
            _password = password;
        }

        private Url GetBaseUrl() => new Url(_url).AppendPathSegment("/rest/api/1.0");

        private async Task<IEnumerable<T>> GetPagedResultsAsync<T>(int? maxPages, IDictionary<string, object> queryParamValues, Func<IDictionary<string, object>, Task<BitbucketResult<T>>> selector)
        {
            var results = new List<T>();
            bool isLastPage = false;
            int numPages = 0;

            while (!isLastPage && (maxPages == null || numPages < maxPages))
            {
                var bbresult = await selector(queryParamValues).ConfigureAwait(false);
                results.AddRange(bbresult.Values);

                isLastPage = bbresult.IsLastPage;
                if (!isLastPage)
                {
                    queryParamValues["start"] = bbresult.NextPageStart;
                }

                numPages++;
            }

            return results;
        }

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
                await GetBaseUrl()
                    .AppendPathSegment("/projects")
                    .WithBasicAuth(_userName, _password)
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
                await GetBaseUrl()
                    .AppendPathSegment($"/projects/{projectKey}/repos")
                    .SetQueryParams(queryParamValues)
                    .WithBasicAuth(_userName, _password)
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
                    await GetBaseUrl()
                        .AppendPathSegment($"/projects/{projectKey}/repos/{repositorySlug}/permissions/groups")
                        .SetQueryParams(queryParamValues)
                        .WithBasicAuth(_userName, _password)
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
                    await GetBaseUrl()
                        .AppendPathSegment($"/projects/{projectKey}/repos/{repositorySlug}/permissions/users")
                        .SetQueryParams(queryParamValues)
                        .WithBasicAuth(_userName, _password)
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
                await GetBaseUrl()
                    .AppendPathSegment($"/projects/{projectKey}/repos/{repositorySlug}/branches")
                    .SetQueryParams(queryParamValues)
                    .WithBasicAuth(_userName, _password)
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
                await GetBaseUrl()
                    .AppendPathSegment($"/projects/{projectKey}/repos/{repositorySlug}/pull-requests")
                    .SetQueryParams(queryParamValues)
                    .WithBasicAuth(_userName, _password)
                    .GetJsonAsync<BitbucketResult<PullRequest>>()
                    .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<PullRequest> GetPullRequestAsync(string projectKey, string repositorySlug, int id)
        {
            return await GetBaseUrl()
                .AppendPathSegment($"/projects/{projectKey}/repos/{repositorySlug}/pull-requests/{id}")
                .WithBasicAuth(_userName, _password)
                .GetJsonAsync<PullRequest>()
                .ConfigureAwait(false);
        }
    }
}
