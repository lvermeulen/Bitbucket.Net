using System.Threading.Tasks;
using Bitbucket.Net.Models.Admin;
using Flurl.Http;

namespace Bitbucket.Net
{
    public partial class BitbucketClient
    {
        private IFlurlRequest GetAdminUrl() => GetBaseUrl()
            .AppendPathSegment("/admin");

        private IFlurlRequest GetAdminUrl(string path) => GetAdminUrl()
            .AppendPathSegment(path);

        public async Task<Cluster> GetClusterAsync()
        {
            return await GetAdminUrl("/cluster")
                .GetJsonAsync<Cluster>()
                .ConfigureAwait(false);
        }

        public async Task<LicenseDetails> GetLicenseAsync()
        {
            return await GetAdminUrl("/license")
                .GetJsonAsync<LicenseDetails>()
                .ConfigureAwait(false);
        }

        public async Task<LicenseDetails> UpdateLicenseAsync(LicenseInfo licenseInfo)
        {
            var response = await GetProjectsUrl()
                .ConfigureRequest(settings => settings.JsonSerializer = s_serializer)
                .PostJsonAsync(licenseInfo)
                .ConfigureAwait(false);

            return await HandleResponseAsync<LicenseDetails>(response).ConfigureAwait(false);
        }
    }
}
