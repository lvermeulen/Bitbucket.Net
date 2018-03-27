using System.Collections.Generic;
using System.Threading.Tasks;
using Bitbucket.Net.Common;
using Bitbucket.Net.Common.Models;
using Bitbucket.Net.Models.Core.Admin;
using Bitbucket.Net.Models.Core.Projects;
using Flurl.Http;

namespace Bitbucket.Net
{
    public partial class BitbucketClient
    {
        private IFlurlRequest GetReposUrl() => GetBaseUrl()
            .AppendPathSegment("/repos");

        public async Task<IEnumerable<Repository>> GetRepositoriesAsync(
            int? maxPages = null,
            int? limit = null,
            int? start = null,
            string name = null,
            string projectName = null,
            Permissions? permission = null,
            bool isPublic = false)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start,
                ["name"] = name,
                ["projectname"] = projectName,
                ["permission"] = BitbucketHelpers.PermissionToString(permission),
                ["visibility"] = isPublic ? "public" : "private"
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                await GetReposUrl()
                    .SetQueryParams(qpv)
                    .GetJsonAsync<PagedResults<Repository>>()
                    .ConfigureAwait(false))
                .ConfigureAwait(false);
        }
    }
}
