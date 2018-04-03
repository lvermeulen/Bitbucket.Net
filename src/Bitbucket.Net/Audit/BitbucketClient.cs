using System.Collections.Generic;
using System.Threading.Tasks;
using Bitbucket.Net.Common.Models;
using Bitbucket.Net.Models.Audit;
using Flurl.Http;

namespace Bitbucket.Net
{
    public partial class BitbucketClient
    {
        private IFlurlRequest GetAuditUrl() => GetBaseUrl("/audit");

        private IFlurlRequest GetAuditUrl(string path) => GetAuditUrl()
            .AppendPathSegment(path);

        public async Task<IEnumerable<AuditEvent>> GetProjectAuditEventsAsync(string projectKey,
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
                    await GetAuditUrl($"/projects/{projectKey}/events")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<PagedResults<AuditEvent>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<AuditEvent>> GetProjectRepoAuditEventsAsync(string projectKey, string repositorySlug,
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
                    await GetAuditUrl($"/projects/{projectKey}/repos/{repositorySlug}/events")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<PagedResults<AuditEvent>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }
    }
}
