using System.Collections.Generic;
using System.Threading.Tasks;
using Bitbucket.Net.Common;
using Bitbucket.Net.Common.Models;
using Bitbucket.Net.Models.Builds;
using Flurl.Http;

namespace Bitbucket.Net
{
    public partial class BitbucketClient
    {
        private IFlurlRequest GetBuildsUrl() => GetBaseUrl("/build-status");

        private IFlurlRequest GetBuildsUrl(string path) => GetBuildsUrl()
            .AppendPathSegment(path);

        public async Task<BuildStats> GetBuildStatsForCommitAsync(string commitId, bool includeUnique = false)
        {
            return await GetBuildsUrl($"/commits/stats/{commitId}")
                .SetQueryParam("includeUnique", BitbucketHelpers.BoolToString(includeUnique))
                .GetJsonAsync<BuildStats>()
                .ConfigureAwait(false);
        }

        public async Task<Dictionary<string, BuildStats>> GetBuildStatsForCommitsAsync(params string[] commitIds)
        {
            var response = await GetBuildsUrl("/commits/stats")
                .PostJsonAsync(commitIds)
                .ConfigureAwait(false);

            return await HandleResponseAsync<Dictionary<string, BuildStats>>(response).ConfigureAwait(false);
        }

        public async Task<IEnumerable<BuildStatus>> GetBuildStatusForCommitAsync(string commitId,
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
                    await GetBuildsUrl($"/commits/{commitId}")
                        .GetJsonAsync<PagedResults<BuildStatus>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<bool> AssociateBuildStatusWithCommitAsync(string commitId, BuildStatus buildStatus)
        {
            var response = await GetBuildsUrl($"/commits/{commitId}")
                .PostJsonAsync(buildStatus)
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }
    }
}
