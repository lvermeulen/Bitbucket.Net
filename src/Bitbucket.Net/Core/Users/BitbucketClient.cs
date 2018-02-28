using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bitbucket.Net.Common.Models;
using Bitbucket.Net.Core.Models.Users;
using Flurl.Http;

namespace Bitbucket.Net.Core
{
    public partial class BitbucketClient
    {
        private IFlurlRequest GetUsersUrl() => GetBaseUrl()
            .AppendPathSegment("/users");

        private IFlurlRequest GetUsersUrl(string path) => GetUsersUrl()
            .AppendPathSegment(path);

        public async Task<IEnumerable<User>> GetUsersAsync(string filter = null, string group = null, string permission = null,
            int? maxPages = null,
            int? limit = null,
            int? start = null,
            params string[] permissionN)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start,
                ["filter"] = filter,
                ["group"] = group,
                ["permission"] = permission
            };

            int permissionNCounter = 0;
            foreach (string perm in permissionN)
            {
                permissionNCounter++;
                queryParamValues.Add($"permission.{permissionNCounter}", perm);
            }

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                    await GetUsersUrl()
                        .SetQueryParams(qpv)
                        .GetJsonAsync<BitbucketResult<User>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<User> UpdateUserAsync(string email = null, string displayName = null)
        {
            var obj = new
            {
                displayName,
                email
            };

            var response = await GetUsersUrl()
                .ConfigureRequest(settings => settings.JsonSerializer = s_serializer)
                .PutJsonAsync(obj)
                .ConfigureAwait(false);

            return await HandleResponseAsync<User>(response).ConfigureAwait(false);
        }

        public async Task<bool> UpdateUserCredentialsAsync(PasswordChange passwordChange)
        {
            var response = await GetUsersUrl("/credentials")
                .ConfigureRequest(settings => settings.JsonSerializer = s_serializer)
                .PutJsonAsync(passwordChange)
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<User> GetUserAsync(string userSlug)
        {
            return await GetUsersUrl($"/{userSlug}")
                .ConfigureRequest(settings => settings.JsonSerializer = s_serializer)
                .GetJsonAsync<User>()
                .ConfigureAwait(false);
        }

        public async Task<bool> DeleteUserAvatarAsync(string userSlug)
        {
            var response = await GetUsersUrl($"/{userSlug}/avatar.png")
                .DeleteAsync()
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<IDictionary<string, object>> GetUserSettingsAsync(string userSlug)
        {
            var response = await GetUsersUrl($"/{userSlug}/settings")
                .ConfigureRequest(settings => settings.JsonSerializer = s_serializer)
                .GetJsonAsync<Dictionary<string, dynamic>>()
                .ConfigureAwait(false);

            return response.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        public async Task<bool> UpdateUserSettingsAsync(string userSlug, IDictionary<string, object> userSettings)
        {
            var response = await GetUsersUrl($"/{userSlug}/settings")
                .ConfigureRequest(settings => settings.JsonSerializer = s_serializer)
                .PostJsonAsync(userSettings)
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }
    }
}
