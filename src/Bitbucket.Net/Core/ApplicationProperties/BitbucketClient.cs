using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flurl.Http;

namespace Bitbucket.Net.Core
{
    public partial class BitbucketClient
    {
        private IFlurlRequest GetApplicationPropertiesUrl() => UrlBuilderExtensions.AppendPathSegment(GetBaseUrl(), "/application-properties");

        public async Task<IDictionary<string, object>> GetApplicationPropertiesAsync()
        {
            var response = await GetApplicationPropertiesUrl()
                .GetJsonAsync<Dictionary<string, dynamic>>()
                .ConfigureAwait(false);

            return response.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }
    }
}
