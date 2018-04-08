using System.Collections.Generic;
using System.Threading.Tasks;
using Bitbucket.Net.Common.Models;
using Bitbucket.Net.Models.PersonalAccessTokens;
using Flurl.Http;

namespace Bitbucket.Net
{
    public partial class BitbucketClient
    {
        private IFlurlRequest GetPatUrl() => GetBaseUrl("/access-tokens");

        private IFlurlRequest GetPatUrl(string path) => GetPatUrl()
            .AppendPathSegment(path);

        public async Task<IEnumerable<AccessToken>> GetUserAccessTokensAsync(string userSlug,
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
                    await GetPatUrl($"/users/{userSlug}")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<PagedResults<AccessToken>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<FullAccessToken> CreateAccessTokenAsync(string userSlug, AccessTokenCreate accessToken)
        {
            var response = await GetPatUrl($"/users/{userSlug}")
                .PutJsonAsync(accessToken)
                .ConfigureAwait(false);

            return await HandleResponseAsync<FullAccessToken>(response).ConfigureAwait(false);
        }

        public async Task<AccessToken> GetUserAccessTokenAsync(string userSlug, string tokenId)
        {
            return await GetPatUrl($"/users/{userSlug}/{tokenId}")
                .GetJsonAsync<AccessToken>()
                .ConfigureAwait(false);
        }

        public async Task<AccessToken> ChangeUserAccessTokenAsync(string userSlug, string tokenId, AccessTokenCreate accessToken)
        {
            var response = await GetPatUrl($"/users/{userSlug}/{tokenId}")
                .PostJsonAsync(accessToken)
                .ConfigureAwait(false);

            return await HandleResponseAsync<FullAccessToken>(response).ConfigureAwait(false);
        }

        public async Task<bool> DeleteUserAccessTokenAsync(string userSlug, string tokenId)
        {
            var response = await GetPatUrl($"/users/{userSlug}/{tokenId}")
                .DeleteAsync()
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }
    }
}
