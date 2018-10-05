using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Bitbucket.Net.Common;
using Bitbucket.Net.Common.Models;
using Bitbucket.Net.Models.Branches;
using Bitbucket.Net.Models.Core.Projects;
using Flurl.Http;
using Newtonsoft.Json;

namespace Bitbucket.Net
{
    public partial class BitbucketClient
    {
        private IFlurlRequest GetBranchUrl() => GetBaseUrl("/branch-utils");

        private IFlurlRequest GetBranchUrl(string path) => GetBranchUrl()
            .AppendPathSegment(path);

        public async Task<IEnumerable<BranchBase>> GetCommitBranchInfoAsync(string projectKey, string repositorySlug, string fullSha,
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
                    await GetBranchUrl($"/projects/{projectKey}/repos/{repositorySlug}/branches/info/{fullSha}")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<PagedResults<BranchBase>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<BranchModel> GetRepoBranchModelAsync(string projectKey, string repositorySlug)
        {
            return await GetBranchUrl($"/projects/{projectKey}/repos/{repositorySlug}/branchmodel")
                .GetJsonAsync<BranchModel>()
                .ConfigureAwait(false);
        }

        public async Task<Branch> CreateRepoBranchAsync(string projectKey, string repositorySlug, string branchName, string startPoint)
        {
            var data = new
            {
                name = branchName,
                startPoint
            };

            var response = await GetBranchUrl($"/projects/{projectKey}/repos/{repositorySlug}/branches")
                .PostJsonAsync(data)
                .ConfigureAwait(false);

            return await HandleResponseAsync<Branch>(response).ConfigureAwait(false);
        }

        public async Task<bool> DeleteRepoBranchAsync(string projectKey, string repositorySlug, string branchName, bool dryRun, string endPoint = null)
        {
            var data = new
            {
                name = branchName,
                dryRun = BitbucketHelpers.BoolToString(dryRun),
                endPoint
            };

            var response = await GetBranchUrl($"/projects/{projectKey}/repos/{repositorySlug}/branches")
                .WithHeader("Content-Type", "application/json")
                .SendAsync(HttpMethod.Delete, new StringContent(JsonConvert.SerializeObject(data)))
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }
    }
}
