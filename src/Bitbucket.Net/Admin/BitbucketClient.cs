using System.Threading.Tasks;
using Bitbucket.Net.Models.Admin;
using Flurl.Http;

namespace Bitbucket.Net
{
    public partial class BitbucketClient
    {
        private IFlurlRequest GetAdminUrl() => GetBaseUrl()
            .AppendPathSegment("/admin")
            .WithBasicAuth(_userName, _password);

        private IFlurlRequest GetAdminUrl(string path) => GetAdminUrl()
            .AppendPathSegment(path);

        public async Task<Cluster> GetClusterAsync()
        {
            return await GetAdminUrl("/cluster")
                .GetJsonAsync<Cluster>()
                .ConfigureAwait(false);
        }
    }


}
