using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Bitbucket.Net.Models.Common;
using Flurl;
using Newtonsoft.Json;

namespace Bitbucket.Net
{
    public partial class BitbucketClient
    {
        private readonly Url _url;
        private readonly string _userName;
        private readonly string _password;

        public BitbucketClient(string url, string userName, string password)
        {
            _url = url;
            _userName = userName;
            _password = password;
        }

        private Url GetBaseUrl() => new Url(_url).AppendPathSegment("/rest/api/1.0");

        private async Task<TResult> ReadResponseContentAsync<TResult>(HttpResponseMessage responseMessage)
        {
            string content = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<TResult>(content);
        }

        private async Task<bool> ReadResponseContentAsync(HttpResponseMessage responseMessage)
        {
            string content = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return content == "";
        }

        private async Task HandleErrorsAsync(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await ReadResponseContentAsync<ErrorResponse>(response).ConfigureAwait(false);
                string errorMessage = string.Join(Environment.NewLine, errorResponse.Errors.Select(x => x.Message));
                throw new InvalidOperationException($"Http request failed ({(int)response.StatusCode} - {response.StatusCode}):\n{errorMessage}");
            }
        }

        private async Task<TResult> HandleResponseAsync<TResult>(HttpResponseMessage responseMessage)
        {
            await HandleErrorsAsync(responseMessage).ConfigureAwait(false);
            return await ReadResponseContentAsync<TResult>(responseMessage).ConfigureAwait(false);
        }

        private async Task<bool> HandleResponseAsync(HttpResponseMessage responseMessage)
        {
            await HandleErrorsAsync(responseMessage).ConfigureAwait(false);
            return await ReadResponseContentAsync(responseMessage).ConfigureAwait(false);
        }

        private async Task<IEnumerable<T>> GetPagedResultsAsync<T>(int? maxPages, IDictionary<string, object> queryParamValues, Func<IDictionary<string, object>, Task<BitbucketResult<T>>> selector)
        {
            var results = new List<T>();
            bool isLastPage = false;
            int numPages = 0;

            while (!isLastPage && (maxPages == null || numPages < maxPages))
            {
                var bbresult = await selector(queryParamValues).ConfigureAwait(false);
                results.AddRange(bbresult.Values);

                isLastPage = bbresult.IsLastPage;
                if (!isLastPage)
                {
                    queryParamValues["start"] = bbresult.NextPageStart;
                }

                numPages++;
            }

            return results;
        }
    }
}
