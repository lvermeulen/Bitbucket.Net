using System.Collections.Generic;
using System.Threading.Tasks;
using Bitbucket.Net.Common;
using Bitbucket.Net.Common.Models;
using Bitbucket.Net.Models.RefRestrictions;
using Flurl.Http;

namespace Bitbucket.Net
{
    public partial class BitbucketClient
    {
        private IFlurlRequest GetRefRestrictionsUrl() => GetBaseUrl("/branch-permissions", "2.0");

        private IFlurlRequest GetRefRestrictionsUrl(string path) => GetRefRestrictionsUrl()
            .AppendPathSegment(path);

        public async Task<IEnumerable<RefRestriction>> GetProjectRefRestrictionsAsync(string projectKey,
            RefRestrictionTypes? type = null,
            RefMatcherTypes? matcherType = null,
            string matcherId = null,
            int? maxPages = null,
            int? limit = null,
            int? start = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["type"] = BitbucketHelpers.RefRestrictionTypeToString(type),
                ["matcherType"] = BitbucketHelpers.RefMatcherTypeToString(matcherType),
                ["matcherId"] = matcherId,
                ["limit"] = limit,
                ["start"] = start
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                    await GetRefRestrictionsUrl($"/projects/{projectKey}/restrictions")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<PagedResults<RefRestriction>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<RefRestriction>> CreateProjectRefRestrictionsAsync(string projectKey, params RefRestrictionCreate[] refRestrictions)
        {
            var response = await GetRefRestrictionsUrl($"/projects/{projectKey}/restrictions")
                .WithHeader("Accept", "application/vnd.atl.bitbucket.bulk+json")
                .PostJsonAsync(refRestrictions)
                .ConfigureAwait(false);

            return await HandleResponseAsync<IEnumerable<RefRestriction>>(response).ConfigureAwait(false);
        }

        public async Task<RefRestriction> CreateProjectRefRestrictionAsync(string projectKey, RefRestrictionCreate refRestriction)
        {
            var response = await GetRefRestrictionsUrl($"/projects/{projectKey}/restrictions")
                .PostJsonAsync(refRestriction)
                .ConfigureAwait(false);

            return await HandleResponseAsync<RefRestriction>(response).ConfigureAwait(false);
        }

        public async Task<RefRestriction> GetProjectRefRestrictionAsync(string projectKey, int refRestrictionId)
        {
            return await GetRefRestrictionsUrl($"/projects/{projectKey}/restrictions/{refRestrictionId}")
                .GetJsonAsync<RefRestriction>()
                .ConfigureAwait(false);
        }

        public async Task<bool> DeleteProjectRefRestrictionAsync(string projectKey, int refRestrictionId)
        {
            var response = await GetRefRestrictionsUrl($"/projects/{projectKey}/restrictions/{refRestrictionId}")
                .DeleteAsync()
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<IEnumerable<RefRestriction>> GetRepositoryRefRestrictionsAsync(string projectKey, string repositorySlug,
            RefRestrictionTypes? type = null,
            RefMatcherTypes? matcherType = null,
            string matcherId = null,
            int? maxPages = null,
            int? limit = null,
            int? start = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["type"] = BitbucketHelpers.RefRestrictionTypeToString(type),
                ["matcherType"] = BitbucketHelpers.RefMatcherTypeToString(matcherType),
                ["matcherId"] = matcherId,
                ["limit"] = limit,
                ["start"] = start
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                    await GetRefRestrictionsUrl($"/projects/{projectKey}/repos/{repositorySlug}/restrictions")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<PagedResults<RefRestriction>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<RefRestriction>> CreateRepositoryRefRestrictionsAsync(string projectKey, string repositorySlug, params RefRestrictionCreate[] refRestrictions)
        {
            var response = await GetRefRestrictionsUrl($"/projects/{projectKey}/repos/{repositorySlug}/restrictions")
                .WithHeader("Accept", "application/vnd.atl.bitbucket.bulk+json")
                .PostJsonAsync(refRestrictions)
                .ConfigureAwait(false);

            return await HandleResponseAsync<IEnumerable<RefRestriction>>(response).ConfigureAwait(false);
        }

        public async Task<RefRestriction> CreateRepositoryRefRestrictionAsync(string projectKey, string repositorySlug, RefRestrictionCreate refRestriction)
        {
            var response = await GetRefRestrictionsUrl($"/projects/{projectKey}/repos/{repositorySlug}/restrictions")
                .PostJsonAsync(refRestriction)
                .ConfigureAwait(false);

            return await HandleResponseAsync<RefRestriction>(response).ConfigureAwait(false);
        }

        public async Task<RefRestriction> GetRepositoryRefRestrictionAsync(string projectKey, string repositorySlug, int refRestrictionId)
        {
            return await GetRefRestrictionsUrl($"/projects/{projectKey}/repos/{repositorySlug}/restrictions/{refRestrictionId}")
                .GetJsonAsync<RefRestriction>()
                .ConfigureAwait(false);
        }

        public async Task<bool> DeleteRepositoryRefRestrictionAsync(string projectKey, string repositorySlug, int refRestrictionId)
        {
            var response = await GetRefRestrictionsUrl($"/projects/{projectKey}/repos/{repositorySlug}/restrictions/{refRestrictionId}")
                .DeleteAsync()
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }
    }
}
