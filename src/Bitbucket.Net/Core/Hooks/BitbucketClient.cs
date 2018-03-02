using System.Threading.Tasks;
using Flurl.Http;

namespace Bitbucket.Net.Core
{
    public partial class BitbucketClient
    {
        private IFlurlRequest GetHooksUrl() => GetBaseUrl()
            .AppendPathSegment("/hooks");

        public async Task<string> GetProjectHooksAvatarAsync(string hookKey, string version = null)
        {
            return await GetHooksUrl()
                .AppendPathSegment($"/{hookKey}/avatar")
                .SetQueryParam("version", version)
                .GetStringAsync()
                .ConfigureAwait(false);
        }
    }
}
