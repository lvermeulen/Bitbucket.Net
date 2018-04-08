using System.Collections.Generic;
using System.Threading.Tasks;
using Bitbucket.Net.Common.Models;
using Bitbucket.Net.Models.Builds;
using Bitbucket.Net.Models.Jira;
using Flurl.Http;

namespace Bitbucket.Net
{
    public partial class BitbucketClient
    {
        private IFlurlRequest GetJiraUrl() => GetBaseUrl("/jira");

        private IFlurlRequest GetJiraUrl(string path) => GetJiraUrl()
            .AppendPathSegment(path);

        public async Task<IEnumerable<ChangeSet>> GetChangeSetsAsync(string issueKey, int maxChanges = 10,
            int? maxPages = null,
            int? limit = null,
            int? start = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start,
                ["maxChanges"] = maxChanges
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                    await GetJiraUrl($"/issues/{issueKey}/commits")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<PagedResults<ChangeSet>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<JiraIssue> CreateJiraIssueAsync(string pullRequestCommentId, string applicationId, string title, string type)
        {
            var data = new
            {
                id = "https://docs.atlassian.com/jira/REST/schema/string#",
                title,
                type
            };

            var response = await GetJiraUrl($"/comments/{pullRequestCommentId}/issues")
                .SetQueryParam("applicationId", applicationId)
                .PostJsonAsync(data)
                .ConfigureAwait(false);

            return await HandleResponseAsync<JiraIssue>(response).ConfigureAwait(false);
        }

        public async Task<IEnumerable<KeyedUrl>> GetJiraIssuesAsync(string projectKey, string repositorySlug, long pullRequestId)
        {
            return await GetJiraUrl($"/projects/{projectKey}/repos/{repositorySlug}/pull-requests/{pullRequestId}/issues")
                .GetJsonAsync<IEnumerable<KeyedUrl>>()
                .ConfigureAwait(false);
        }
    }
}
