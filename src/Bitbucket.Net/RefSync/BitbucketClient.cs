using System.Threading.Tasks;
using Bitbucket.Net.Common;
using Bitbucket.Net.Models.RefSync;
using Flurl.Http;

namespace Bitbucket.Net
{
    public partial class BitbucketClient
    {
        private IFlurlRequest GetRefSyncUrl() => GetBaseUrl("/sync");

        private IFlurlRequest GetRefSyncUrl(string path) => GetRefSyncUrl()
            .AppendPathSegment(path);

        public async Task<RepositorySynchronizationStatus> GetRepositorySynchronizationStatusAsync(string projectKey, string repositorySlug,
            string at = null)
        {
            return await GetRefSyncUrl($"/projects/{projectKey}/repos/{repositorySlug}")
                .SetQueryParam("at", at)
                .GetJsonAsync<RepositorySynchronizationStatus>()
                .ConfigureAwait(false);
        }

        public async Task<RepositorySynchronizationStatus> EnableRepositorySynchronizationAsync(string projectKey, string repositorySlug, bool enabled)
        {
            var data = new
            {
                enabled = BitbucketHelpers.BoolToString(enabled)
            };

            var response = await GetRefSyncUrl($"/projects/{projectKey}/repos/{repositorySlug}")
                .PostJsonAsync(data)
                .ConfigureAwait(false);

            return await HandleResponseAsync<RepositorySynchronizationStatus>(response).ConfigureAwait(false);
        }

        public async Task<FullRef> SynchronizeRepositoryAsync(string projectKey, string repositorySlug, Synchronize synchronize)
        {
            var response = await GetRefSyncUrl($"/projects/{projectKey}/repos/{repositorySlug}")
                .PostJsonAsync(synchronize)
                .ConfigureAwait(false);

            return await HandleResponseAsync<FullRef>(response).ConfigureAwait(false);
        }
    }
}
