using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Bitbucket.Net.Common;
using Bitbucket.Net.Common.Models;
using Bitbucket.Net.Models.Core.Admin;
using Bitbucket.Net.Models.Core.Users;
using Flurl.Http;
using PasswordChange = Bitbucket.Net.Models.Core.Admin.PasswordChange;

namespace Bitbucket.Net
{
    public partial class BitbucketClient
    {
        private IFlurlRequest GetAdminUrl() => GetBaseUrl()
            .AppendPathSegment("/admin");

        private IFlurlRequest GetAdminUrl(string path) => GetAdminUrl()
            .AppendPathSegment(path);

        public async Task<IEnumerable<DeletableGroupOrUser>> GetAdminGroupsAsync(string filter = null,
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
                        .GetJsonAsync<PagedResults<DeletableGroupOrUser>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<DeletableGroupOrUser> CreateAdminGroupAsync(string name)
        {
            var response = await GetAdminUrl("/groups")
                .SetQueryParam("name", name)
                .PostJsonAsync(new StringContent(""))
                .ConfigureAwait(false);

            return await HandleResponseAsync<DeletableGroupOrUser>(response).ConfigureAwait(false);
        }

        public async Task<DeletableGroupOrUser> DeleteAdminGroupAsync(string name)
        {
            var response = await GetAdminUrl("/groups")
                .SetQueryParam("name", name)
                .DeleteAsync()
                .ConfigureAwait(false);

            return await HandleResponseAsync<DeletableGroupOrUser>(response).ConfigureAwait(false);
        }

        public async Task<bool> AddAdminGroupUsersAsync(GroupUsers groupUsers)
        {
            var response = await GetAdminUrl("/groups/add-users")
                .PostJsonAsync(groupUsers)
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
                        .GetJsonAsync<PagedResults<UserInfo>>()
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
                    await GetAdminUrl("/groups/more-non-members")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<PagedResults<UserInfo>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<UserInfo>> GetAdminUsersAsync(string filter = null,
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
                    await GetAdminUrl("/users")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<PagedResults<UserInfo>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<bool> CreateAdminUserAsync(string name, string password, string displayName, string emailAddress,
            bool addToDefaultGroup = true, string notify = "false")
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["name"] = name,
                ["password"] = password,
                ["displayName"] = displayName,
                ["emailAddress"] = emailAddress,
                ["addToDefaultGroup"] = BitbucketHelpers.BoolToString(addToDefaultGroup),
                ["notify"] = notify
            };

            var response = await GetAdminUrl("/users")
                .SetQueryParams(queryParamValues)
                .PostJsonAsync(new StringContent(""))
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<UserInfo> UpdateAdminUserAsync(string name = null, string displayName = null, string emailAddress = null)
        {
            var data = new DynamicDictionary
            {
                { name, "name" },
                { displayName, "displayName" },
                { emailAddress, "email" }
            };

            var response = await GetAdminUrl("/users")
                .PutJsonAsync(data.ToDictionary())
                .ConfigureAwait(false);

            return await HandleResponseAsync<UserInfo>(response).ConfigureAwait(false);
        }

        public async Task<UserInfo> DeleteAdminUserAsync(string name)
        {
            var response = await GetAdminUrl("/users")
                .SetQueryParam("name", name)
                .DeleteAsync()
                .ConfigureAwait(false);

            return await HandleResponseAsync<UserInfo>(response).ConfigureAwait(false);
        }

        public async Task<bool> AddAdminUserGroupsAsync(UserGroups userGroups)
        {
            var response = await GetAdminUrl("/users/add-groups")
                .PostJsonAsync(userGroups)
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<bool> DeleteAdminUserCaptcha(string name)
        {
            var response = await GetAdminUrl("/users/captcha")
                .SetQueryParam("name", name)
                .DeleteAsync()
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<bool> UpdateAdminUserCredentialsAsync(PasswordChange passwordChange)
        {
            var response = await GetAdminUrl("/users/credentials")
                .PutJsonAsync(passwordChange)
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<IEnumerable<DeletableGroupOrUser>> GetAdminUserMoreMembersAsync(string context, string filter = null,
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
                    await GetAdminUrl("/users/more-members")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<PagedResults<DeletableGroupOrUser>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<DeletableGroupOrUser>> GetAdminUserMoreNonMembersAsync(string context, string filter = null,
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
                    await GetAdminUrl("/users/more-non-members")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<PagedResults<DeletableGroupOrUser>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<bool> RemoveAdminUserFromGroupAsync(string userName, string groupName)
        {
            var data = new
            {
                context = userName,
                itemName = groupName
            };

            var response = await GetAdminUrl("/users/remove-group")
                .PostJsonAsync(data)
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<UserInfo> RenameAdminUserAsync(UserRename userRename)
        {
            var response = await GetAdminUrl("users/rename")
                .PostJsonAsync(userRename)
                .ConfigureAwait(false);

            return await HandleResponseAsync<UserInfo>(response).ConfigureAwait(false);
        }

        public async Task<Cluster> GetAdminClusterAsync()
        {
            return await GetAdminUrl("/cluster")
                .GetJsonAsync<Cluster>()
                .ConfigureAwait(false);
        }

        public async Task<LicenseDetails> GetAdminLicenseAsync()
        {
            return await GetAdminUrl("/license")
                .GetJsonAsync<LicenseDetails>()
                .ConfigureAwait(false);
        }

        public async Task<LicenseDetails> UpdateAdminLicenseAsync(LicenseInfo licenseInfo)
        {
            var response = await GetAdminUrl("/license")
                .PostJsonAsync(licenseInfo)
                .ConfigureAwait(false);

            return await HandleResponseAsync<LicenseDetails>(response).ConfigureAwait(false);
        }

        public async Task<MailServerConfiguration> GetAdminMailServerAsync()
        {
            return await GetAdminUrl("/mail-server")
                .GetJsonAsync<MailServerConfiguration>()
                .ConfigureAwait(false);
        }

        public async Task<MailServerConfiguration> UpdateAdminMailServerAsync(MailServerConfiguration mailServerConfiguration)
        {
            var response = await GetAdminUrl("/mail-server")
                .PutJsonAsync(mailServerConfiguration)
                .ConfigureAwait(false);

            return await HandleResponseAsync<MailServerConfiguration>(response).ConfigureAwait(false);
        }

        public async Task<bool> DeleteAdminMailServerAsync()
        {
            var response = await GetAdminUrl("/mail-server")
                .DeleteAsync()
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<string> GetAdminMailServerSenderAddressAsync()
        {
            var response = await GetAdminUrl("/mail-server/sender-address")
                .GetAsync()
                .ConfigureAwait(false);

            return await HandleResponseAsync(response, s => s).ConfigureAwait(false);
        }

        public async Task<string> UpdateAdminMailServerSenderAddressAsync(string senderAddress)
        {
            var response = await GetAdminUrl("/mail-server/sender-address")
                .PutJsonAsync(senderAddress)
                .ConfigureAwait(false);

            return await HandleResponseAsync(response, s => s).ConfigureAwait(false);
        }

        public async Task<bool> DeleteAdminMailServerSenderAddressAsync()
        {
            var response = await GetAdminUrl("/mail-server/sender-address")
                .DeleteAsync()
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<IEnumerable<GroupPermission>> GetAdminGroupPermissionsAsync(string filter = null,
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
                    await GetAdminUrl("/permissions/groups")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<PagedResults<GroupPermission>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<bool> UpdateAdminGroupPermissionsAsync(Permissions permission, string name)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["permission"] = permission,
                ["name"] = name
            };

            var response = await GetAdminUrl("/permissions/groups")
                .SetQueryParams(queryParamValues)
                .PutJsonAsync(new StringContent(""))
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<bool> DeleteAdminGroupPermissionsAsync(string name)
        {
            var response = await GetAdminUrl("/permissions/groups")
                .SetQueryParam("name", name)
                .DeleteAsync()
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<IEnumerable<DeletableGroupOrUser>> GetAdminGroupPermissionsNoneAsync(string filter = null,
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
                    await GetAdminUrl("/permissions/groups/none")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<PagedResults<DeletableGroupOrUser>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<UserPermission>> GetAdminUserPermissionsAsync(string filter = null,
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
                    await GetAdminUrl("/permissions/users")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<PagedResults<UserPermission>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<bool> UpdateAdminUserPermissionsAsync(Permissions permission, string name)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["permission"] = permission,
                ["name"] = name
            };

            var response = await GetAdminUrl("/permissions/users")
                .SetQueryParams(queryParamValues)
                .PutJsonAsync(new StringContent(""))
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<bool> DeleteAdminUserPermissionsAsync(string name)
        {
            var response = await GetAdminUrl("/permissions/users")
                .SetQueryParam("name", name)
                .DeleteAsync()
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<IEnumerable<User>> GetAdminUserPermissionsNoneAsync(string filter = null,
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
                    await GetAdminUrl("/permissions/users/none")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<PagedResults<User>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<MergeStrategies> GetAdminPullRequestsMergeStrategiesAsync(string scmId)
        {
            return await GetAdminUrl($"/pull-requests/{scmId}")
                .GetJsonAsync<MergeStrategies>()
                .ConfigureAwait(false);
        }

        public async Task<MergeStrategies> UpdateAdminPullRequestsMergeStrategiesAsync(string scmId, MergeStrategies mergeStrategies)
        {
            var response = await GetAdminUrl($"/pull-requests/{scmId}")
                .PostJsonAsync(mergeStrategies)
                .ConfigureAwait(false);

            return await HandleResponseAsync<MergeStrategies>(response).ConfigureAwait(false);
        }
    }
}
