using System.Collections.Generic;
using System.Threading.Tasks;
using Bitbucket.Net.Common;
using Bitbucket.Net.Common.Models;
using Bitbucket.Net.Models.Core.Projects;
using Flurl.Http;
using Newtonsoft.Json;

namespace Bitbucket.Net
{
    public partial class BitbucketClient
    {
        private IFlurlRequest GetInboxUrl() => GetBaseUrl()
            .AppendPathSegment("/inbox");

        private IFlurlRequest GetInboxUrl(string path) => GetInboxUrl()
            .AppendPathSegment(path);

        public async Task<IEnumerable<PullRequest>> GetInboxPullRequestsAsync(
            int? maxPages = null,
            int? limit = 25,
            int? start = 0,
            Roles role = Roles.Reviewer)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start,
                ["role"] = BitbucketHelpers.RoleToString(role)
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                await GetInboxUrl("/pull-requests")
                    .SetQueryParams(qpv)
                    .GetJsonAsync<PagedResults<PullRequest>>()
                    .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<int> GetInboxPullRequestsCountAsync()
        {
            var response = await GetInboxUrl("/pull-requests/count")
                .GetAsync()
                .ConfigureAwait(false);

            return await HandleResponseAsync(response, s => 
                    int.TryParse(JsonConvert.DeserializeObject<dynamic>(s).count.ToString(), out int result) ? result : -1)
                .ConfigureAwait(false);
        }
    }
}
