using System.Net.Http;
using System.Threading.Tasks;
using Bitbucket.Net.Common;
using Bitbucket.Net.Core.Models.Logs;
using Flurl.Http;
using Newtonsoft.Json;

namespace Bitbucket.Net.Core
{
    public partial class BitbucketClient
    {
        private IFlurlRequest GetLogsUrl() => UrlBuilderExtensions.AppendPathSegment(GetBaseUrl(), "/logs");

        private IFlurlRequest GetLogsUrl(string path) => GetLogsUrl()
            .AppendPathSegment(path);

        public async Task<LogLevels> GetLogLevelAsync(string loggerName)
        {
            var response = await GetLogsUrl($"/logger/{loggerName}")
                .GetAsync()
                .ConfigureAwait(false);

            return await HandleResponseAsync<LogLevels>(response, s =>
                    BitbucketHelpers.StringToLogLevel(JsonConvert.DeserializeObject<dynamic>(s).logLevel.ToString()))
                .ConfigureAwait(false);
        }

        public async Task<bool> SetLogLevelAsync(string loggerName, LogLevels logLevel)
        {
            var response = await GetLogsUrl($"/logger/{loggerName}/{BitbucketHelpers.LogLevelToString(logLevel)}")
                .PutAsync(new StringContent(""))
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<LogLevels> GetRootLogLevelAsync()
        {
            var response = await GetLogsUrl("/logger/rootLogger")
                .GetAsync()
                .ConfigureAwait(false);

            return await HandleResponseAsync<LogLevels>(response, s =>
                    BitbucketHelpers.StringToLogLevel(JsonConvert.DeserializeObject<dynamic>(s).logLevel.ToString()))
                .ConfigureAwait(false);
        }

        public async Task<bool> SetRootLogLevelAsync(LogLevels logLevel)
        {
            var response = await GetLogsUrl($"/logger/rootLogger/{BitbucketHelpers.LogLevelToString(logLevel)}")
                .PutAsync(new StringContent(""))
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }
    }
}
