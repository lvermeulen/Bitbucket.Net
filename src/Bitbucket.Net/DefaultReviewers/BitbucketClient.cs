using System.Collections.Generic;
using System.Threading.Tasks;
using Bitbucket.Net.Models.Core.Users;
using Bitbucket.Net.Models.DefaultReviewers;
using Flurl.Http;

namespace Bitbucket.Net
{
    public partial class BitbucketClient
    {
        private IFlurlRequest GetDefaultReviewersUrl() => GetBaseUrl("/default-reviewers");

        private IFlurlRequest GetDefaultReviewersUrl(string path) => GetDefaultReviewersUrl()
            .AppendPathSegment(path);

        public async Task<IEnumerable<DefaultReviewerPullRequestCondition>> GetDefaultReviewerConditionsAsync(string projectKey)
        {
            return await GetDefaultReviewersUrl($"/projects/{projectKey}/conditions")
                .GetJsonAsync<IEnumerable<DefaultReviewerPullRequestCondition>>()
                .ConfigureAwait(false);
        }

        public async Task<DefaultReviewerPullRequestCondition> CreateDefaultReviewerConditionAsync(string projectKey, DefaultReviewerPullRequestCondition condition)
        {
            var response = await GetDefaultReviewersUrl($"/projects/{projectKey}/conditions")
                .PostJsonAsync(condition)
                .ConfigureAwait(false);

            return await HandleResponseAsync<DefaultReviewerPullRequestCondition>(response).ConfigureAwait(false);
        }

        public async Task<DefaultReviewerPullRequestCondition> UpdateDefaultReviewerConditionAsync(string projectKey, string defaultReviewerPullRequestConditionId, DefaultReviewerPullRequestCondition condition)
        {
            var response = await GetDefaultReviewersUrl($"/projects/{projectKey}/conditions/{defaultReviewerPullRequestConditionId}")
                .PutJsonAsync(condition)
                .ConfigureAwait(false);

            return await HandleResponseAsync<DefaultReviewerPullRequestCondition>(response).ConfigureAwait(false);
        }

        public async Task<bool> DeleteDefaultReviewerConditionAsync(string projectKey, string defaultReviewerPullRequestConditionId)
        {
            var response = await GetDefaultReviewersUrl($"/projects/{projectKey}/conditions/{defaultReviewerPullRequestConditionId}")
                .DeleteAsync()
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<IEnumerable<DefaultReviewerPullRequestCondition>> GetDefaultReviewerConditionsAsync(string projectKey, string repositorySlug)
        {
            return await GetDefaultReviewersUrl($"/projects/{projectKey}/repos/{repositorySlug}/conditions")
                .GetJsonAsync<IEnumerable<DefaultReviewerPullRequestCondition>>()
                .ConfigureAwait(false);
        }

        public async Task<DefaultReviewerPullRequestCondition> CreateDefaultReviewerConditionAsync(string projectKey, string repositorySlug, DefaultReviewerPullRequestCondition condition)
        {
            var response = await GetDefaultReviewersUrl($"/projects/{projectKey}/repos/{repositorySlug}/conditions")
                .PostJsonAsync(condition)
                .ConfigureAwait(false);

            return await HandleResponseAsync<DefaultReviewerPullRequestCondition>(response).ConfigureAwait(false);
        }

        public async Task<DefaultReviewerPullRequestCondition> UpdateDefaultReviewerConditionAsync(string projectKey, string repositorySlug, string defaultReviewerPullRequestConditionId, DefaultReviewerPullRequestCondition condition)
        {
            var response = await GetDefaultReviewersUrl($"/projects/{projectKey}/repos/{repositorySlug}/conditions/{defaultReviewerPullRequestConditionId}")
                .PutJsonAsync(condition)
                .ConfigureAwait(false);

            return await HandleResponseAsync<DefaultReviewerPullRequestCondition>(response).ConfigureAwait(false);
        }

        public async Task<bool> DeleteDefaultReviewerConditionAsync(string projectKey, string repositorySlug, string defaultReviewerPullRequestConditionId)
        {
            var response = await GetDefaultReviewersUrl($"/projects/{projectKey}/repos/{repositorySlug}/conditions/{defaultReviewerPullRequestConditionId}")
                .DeleteAsync()
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<IEnumerable<User>> GetDefaultReviewersAsync(string projectKey, string repositorySlug,
            int? sourceRepoId = null,
            int? targetRepoId = null,
            string sourceRefId = null,
            string targetRefId = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["sourceRepoId"] = sourceRepoId,
                ["targetRepoId"] = targetRepoId,
                ["sourceRefId"] = sourceRefId,
                ["targetRefId"] = targetRefId
            };

            return await GetDefaultReviewersUrl($"/projects/{projectKey}/repos/{repositorySlug}/reviewers")
                .SetQueryParams(queryParamValues)
                .GetJsonAsync<IEnumerable<User>>()
                .ConfigureAwait(false);
        }
    }
}
