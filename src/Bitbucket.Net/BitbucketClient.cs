using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Bitbucket.Net.Common.Models;
using Flurl;
using Flurl.Http;
using Flurl.Http.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Bitbucket.Net
{
    public partial class BitbucketClient
    {
        private static readonly ISerializer s_serializer = new NewtonsoftJsonSerializer(new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

        private readonly Url _url;
        private readonly string _userName;
        private readonly string _password;

        public BitbucketClient(string url, string userName, string password)
        {
            _url = url;
            _userName = userName;
            _password = password;
        }

        private IFlurlRequest GetBaseUrl(string root = "/api", string version = "1.0") => new Url(_url)
            .AppendPathSegment($"/rest{root}/{version}")
            .ConfigureRequest(settings => settings.JsonSerializer = s_serializer)
            .WithBasicAuth(_userName, _password);

        private async Task<TResult> ReadResponseContentAsync<TResult>(HttpResponseMessage responseMessage, Func<string, TResult> contentHandler = null)
        {
            string content = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return contentHandler != null 
                ? contentHandler(content) 
                : JsonConvert.DeserializeObject<TResult>(content);
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

        private async Task<TResult> HandleResponseAsync<TResult>(HttpResponseMessage responseMessage, Func<string, TResult> contentHandler = null)
        {
            await HandleErrorsAsync(responseMessage).ConfigureAwait(false);
            return await ReadResponseContentAsync(responseMessage, contentHandler).ConfigureAwait(false);
        }

        private async Task<bool> HandleResponseAsync(HttpResponseMessage responseMessage)
        {
            await HandleErrorsAsync(responseMessage).ConfigureAwait(false);
            return await ReadResponseContentAsync(responseMessage).ConfigureAwait(false);
        }

        private async Task<IEnumerable<T>> GetPagedResultsAsync<T>(int? maxPages, IDictionary<string, object> queryParamValues, Func<IDictionary<string, object>, Task<PagedResults<T>>> selector)
        {
            var results = new List<T>();
            bool isLastPage = false;
            int numPages = 0;

            while (!isLastPage && (maxPages == null || numPages < maxPages))
            {
                var selectorResults = await selector(queryParamValues).ConfigureAwait(false);
                results.AddRange(selectorResults.Values);

                isLastPage = selectorResults.IsLastPage;
                if (!isLastPage)
                {
                    queryParamValues["start"] = selectorResults.NextPageStart;
                }

                numPages++;
            }

            return results;
        }
    }
}
