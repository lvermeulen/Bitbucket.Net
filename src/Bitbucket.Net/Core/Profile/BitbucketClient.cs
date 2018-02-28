using System.Collections.Generic;
using System.Threading.Tasks;
using Bitbucket.Net.Common;
using Bitbucket.Net.Common.Models;
using Bitbucket.Net.Core.Models.Projects;
using Flurl.Http;

namespace Bitbucket.Net.Core
{
    public partial class BitbucketClient
    {
        private IFlurlRequest GetProfileUrl() => GetBaseUrl()
            .AppendPathSegment("/profile");

        private IFlurlRequest GetProfileUrl(string path) => GetProfileUrl()
            .AppendPathSegment(path);

        public async Task<IEnumerable<Repository>> GetRecentReposAsync(Permissions? permission = null,
            int? maxPages = null,
            int? limit = null,
            int? start = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start,
                ["permission"] = BitbucketHelpers.PermissionToString(permission)
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                await GetProfileUrl("/recent/repos")
                    .SetQueryParams(qpv)
                    .GetJsonAsync<BitbucketResult<Repository>>()
                    .ConfigureAwait(false))
                .ConfigureAwait(false);
        }
    }
}
