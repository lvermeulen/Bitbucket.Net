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

        public async Task<MailServerConfiguration> GetMailServerAsync()
        {
            return await GetAdminUrl("/mail-server")
                .GetJsonAsync<MailServerConfiguration>()
                .ConfigureAwait(false);
        }

        public async Task<MailServerConfiguration> UpdateMailServerAsync(MailServerConfiguration mailServerConfiguration)
        {
            var response = await GetAdminUrl("/mail-server")
                .ConfigureRequest(settings => settings.JsonSerializer = s_serializer)
                .PutJsonAsync(mailServerConfiguration)
                .ConfigureAwait(false);

            return await HandleResponseAsync<MailServerConfiguration>(response).ConfigureAwait(false);
        }

        public async Task<bool> DeleteMailServerAsync()
        {
            var response = await GetAdminUrl("/mail-server")
                .DeleteAsync()
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<string> GetMailServerSenderAddressAsync()
        {
            var response = await GetAdminUrl("/mail-server/sender-address")
                .GetAsync()
                .ConfigureAwait(false);

            return await HandleResponseAsync(response, s => s).ConfigureAwait(false);
        }

        public async Task<string> UpdateMailServerSenderAddressAsync(string senderAddress)
        {
            var response = await GetAdminUrl("/mail-server/sender-address")
                .PutJsonAsync(senderAddress)
                .ConfigureAwait(false);

            return await HandleResponseAsync(response, s => s).ConfigureAwait(false);
        }

        public async Task<bool> DeleteMailServerSenderAddressAsync()
        {
            var response = await GetAdminUrl("/mail-server/sender-address")
                .DeleteAsync()
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }
    }
}
