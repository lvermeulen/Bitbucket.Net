using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Bitbucket.Net.Common;
using Flurl.Http;
using Newtonsoft.Json;

namespace Bitbucket.Net
{
    public partial class BitbucketClient
    {
        private IFlurlRequest GetMarkupUrl() => GetBaseUrl()
            .AppendPathSegment("/markup");

        private IFlurlRequest GetMarkupUrl(string path) => GetMarkupUrl()
            .AppendPathSegment(path);

        public async Task<string> PreviewMarkupAsync(string text,
            string urlMode = null, 
            bool? hardWrap = null, 
            bool? htmlEscape = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["urlMode"] = urlMode,
                ["hardWrap"] = BitbucketHelpers.BoolToString(hardWrap),
                ["htmlEscape"] = BitbucketHelpers.BoolToString(htmlEscape)
            };

            var response = await GetMarkupUrl("/preview")
                .WithHeader("X-Atlassian-Token", "no-check")
                .SetQueryParams(queryParamValues)
                .PostJsonAsync(new StringContent(text))
                .ConfigureAwait(false);

            return await HandleResponseAsync<string>(response, s =>
                    JsonConvert.DeserializeObject<dynamic>(s).html.ToString())
                .ConfigureAwait(false);
        }
    }
}
