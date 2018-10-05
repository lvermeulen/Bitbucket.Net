using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Bitbucket.Net.Common;
using Bitbucket.Net.Common.Models;
using Bitbucket.Net.Models.Core.Admin;
using Bitbucket.Net.Models.RefRestrictions;
using Bitbucket.Net.Models.Ssh;
using Flurl.Http;
using Newtonsoft.Json;

namespace Bitbucket.Net
{
    public partial class BitbucketClient
    {
        private IFlurlRequest GetKeysUrl() => GetBaseUrl("/keys");

        private IFlurlRequest GetKeysUrl(string path) => GetKeysUrl()
            .AppendPathSegment(path);

        private IFlurlRequest GetSshUrl() => GetBaseUrl("/ssh");

        private IFlurlRequest GetSshUrl(string path) => GetSshUrl()
            .AppendPathSegment(path);

        public async Task<bool> DeleteProjectsReposKeysAsync(int keyId, params string[] projectsOrRepos)
        {
            var response =  await GetKeysUrl($"/ssh/{keyId}")
                .WithHeader("Content-Type", "application/json")
                .SendAsync(HttpMethod.Delete, new StringContent(JsonConvert.SerializeObject(projectsOrRepos)))
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<IEnumerable<ProjectKey>> GetProjectKeysAsync(int keyId,
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
                    await GetKeysUrl($"/ssh/{keyId}/projects")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<PagedResults<ProjectKey>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<ProjectKey>> GetProjectKeysAsync(string projectKey,
            string filter = null,
            Permissions? permission = null,
            int? maxPages = null,
            int? limit = null,
            int? start = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start,
                ["filter"] = filter,
                ["permission"] = BitbucketHelpers.PermissionToString(permission)
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                    await GetKeysUrl($"/projects/{projectKey}/ssh")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<PagedResults<ProjectKey>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<ProjectKey> CreateProjectKeyAsync(string projectKey, string keyText, Permissions permission)
        {
            var data = new
            {
                key = new { text = keyText },
                permission = BitbucketHelpers.PermissionToString(permission)
            };

            var response = await GetKeysUrl($"/projects/{projectKey}/ssh")
                .PostJsonAsync(data)
                .ConfigureAwait(false);

            return await HandleResponseAsync<ProjectKey>(response).ConfigureAwait(false);
        }

        public async Task<ProjectKey> GetProjectKeyAsync(string projectKey, int keyId)
        {
            return await GetKeysUrl($"/projects/{projectKey}/ssh/{keyId}")
                .GetJsonAsync<ProjectKey>()
                .ConfigureAwait(false);
        }

        public async Task<bool> DeleteProjectKeyAsync(string projectKey, int keyId)
        {
            var response = await GetKeysUrl($"/projects/{projectKey}/ssh/{keyId}")
                .DeleteAsync()
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<ProjectKey> UpdateProjectKeyPermissionAsync(string projectKey, int keyId, Permissions permission)
        {
            var response = await GetKeysUrl($"/projects/{projectKey}/ssh/{keyId}/permissions/{BitbucketHelpers.PermissionToString(permission)}")
                .PutAsync(new StringContent(""))
                .ConfigureAwait(false);

            return await HandleResponseAsync<ProjectKey>(response).ConfigureAwait(false);
        }

        public async Task<IEnumerable<RepositoryKey>> GetRepoKeysAsync(int keyId,
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
                    await GetKeysUrl($"/ssh/{keyId}/repos")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<PagedResults<RepositoryKey>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<RepositoryKey>> GetRepoKeysAsync(string projectKey, string repositorySlug,
            string filter = null,
            bool? effective = null,
            Permissions? permission = null,
            int? maxPages = null,
            int? limit = null,
            int? start = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start,
                ["filter"] = filter,
                ["effective"] = BitbucketHelpers.BoolToString(effective),
                ["permission"] = BitbucketHelpers.PermissionToString(permission)
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                    await GetKeysUrl($"/projects/{projectKey}/repos/{repositorySlug}/ssh")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<PagedResults<RepositoryKey>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<RepositoryKey> CreateRepoKeyAsync(string projectKey, string repositorySlug, string keyText, Permissions permission)
        {
            var data = new
            {
                key = new { text = keyText },
                permission = BitbucketHelpers.PermissionToString(permission)
            };

            var response = await GetKeysUrl($"/projects/{projectKey}/repos/{repositorySlug}/ssh")
                .PostJsonAsync(data)
                .ConfigureAwait(false);

            return await HandleResponseAsync<RepositoryKey>(response).ConfigureAwait(false);
        }

        public async Task<RepositoryKey> GetRepoKeyAsync(string projectKey, string repositorySlug, int keyId)
        {
            return await GetKeysUrl($"/projects/{projectKey}/repos/{repositorySlug}/ssh/{keyId}")
                .GetJsonAsync<RepositoryKey>()
                .ConfigureAwait(false);
        }

        public async Task<bool> DeleteRepoKeyAsync(string projectKey, string repositorySlug, int keyId)
        {
            var response = await GetKeysUrl($"/projects/{projectKey}/repos/{repositorySlug}/ssh/{keyId}")
                .DeleteAsync()
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<RepositoryKey> UpdateRepoKeyPermissionAsync(string projectKey, string repositorySlug, int keyId, Permissions permission)
        {
            var response = await GetKeysUrl($"/projects/{projectKey}/repos/{repositorySlug}/ssh/{keyId}/permissions/{BitbucketHelpers.PermissionToString(permission)}")
                .PutAsync(new StringContent(""))
                .ConfigureAwait(false);

            return await HandleResponseAsync<RepositoryKey>(response).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Key>> GetUserKeysAsync(string userSlug = null,
            int? maxPages = null,
            int? limit = null,
            int? start = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start,
                ["user"] = userSlug
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                    await GetSshUrl("/keys")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<PagedResults<Key>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<Key> CreateUserKeyAsync(string keyText, string userSlug = null)
        {
            var response = await GetSshUrl("/keys")
                .SetQueryParam("user", userSlug)
                .PostJsonAsync(new { text = keyText })
                .ConfigureAwait(false);

            return await HandleResponseAsync<Key>(response).ConfigureAwait(false);
        }

        public async Task<bool> DeleteUserKeysAsync(string userSlug = null)
        {
            var response = await GetSshUrl("/keys")
                .SetQueryParam("user", userSlug)
                .DeleteAsync()
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<bool> DeleteUserKeyAsync(int keyId)
        {
            var response = await GetSshUrl("/keys/{keyId}")
                .DeleteAsync()
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<SshSettings> GetSshSettingsAsync()
        {
            return await GetSshUrl("/settings")
                .GetJsonAsync<SshSettings>()
                .ConfigureAwait(false);
        }
    }
}
