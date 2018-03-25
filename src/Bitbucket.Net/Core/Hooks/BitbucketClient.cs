using System.Threading.Tasks;
using Flurl.Http;

namespace Bitbucket.Net
{
    public partial class BitbucketClient
    {
        private IFlurlRequest GetHooksUrl() => GetBaseUrl()
            .AppendPathSegment("/hooks");

        public async Task<byte[]> GetProjectHooksAvatarAsync(string hookKey, string version = null)
        {
            return await GetHooksUrl()
                .AppendPathSegment($"/{hookKey}/avatar")
                .SetQueryParam("version", version)
                .GetBytesAsync()
                .ConfigureAwait(false);
        }
    }
}
