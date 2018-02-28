using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Bitbucket.Net.Common.Models;
using Bitbucket.Net.Core.Models.Admin;
using Flurl.Http;

namespace Bitbucket.Net.Core
{
    public partial class BitbucketClient
    {
        private IFlurlRequest GetAdminUrl() => GetBaseUrl()
            .AppendPathSegment("/admin");

        private IFlurlRequest GetAdminUrl(string path) => GetAdminUrl()
            .AppendPathSegment(path);

        public async Task<IEnumerable<DeletableGroup>> GetAdminGroupsAsync(string filter = null,
            int? maxPages = null,
            int? limit = null,
            int? start = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start,
                ["filter"] = filter
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                    await GetAdminUrl("/groups")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<BitbucketResult<DeletableGroup>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<DeletableGroup> CreateAdminGroupAsync(string name)
        {
            var response = await GetAdminUrl("/groups")
                .SetQueryParam("name", name)
                .PostAsync(new StringContent(""))
                .ConfigureAwait(false);

            return await HandleResponseAsync<DeletableGroup>(response).ConfigureAwait(false);
        }

        public async Task<DeletableGroup> DeleteAdminGroupAsync(string name)
        {
            var response = await GetAdminUrl("/groups")
                .SetQueryParam("name", name)
                .DeleteAsync()
                .ConfigureAwait(false);

            return await HandleResponseAsync<DeletableGroup>(response).ConfigureAwait(false);
        }

        public async Task<bool> AddAdminGroupUsersAsync(UsersGroup usersGroup)
        {
            var response = await GetAdminUrl("/groups/add-users")
                .ConfigureRequest(settings => settings.JsonSerializer = s_serializer)
                .PostJsonAsync(usersGroup)
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<IEnumerable<UserInfo>> GetAdminGroupMoreMembersAsync(string context, string filter = null,
            int? maxPages = null,
            int? limit = null,
            int? start = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start,
                ["context"] = context,
                ["filter"] = filter
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                    await GetAdminUrl("/groups/more-members")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<BitbucketResult<UserInfo>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<UserInfo>> GetAdminGroupMoreNonMembersAsync(string context, string filter = null,
            int? maxPages = null,
            int? limit = null,
            int? start = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start,
                ["context"] = context,
                ["filter"] = filter
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                    await GetAdminUrl("/groups/more-none-members")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<BitbucketResult<UserInfo>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

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
            var response = await GetAdminUrl("/license")
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
