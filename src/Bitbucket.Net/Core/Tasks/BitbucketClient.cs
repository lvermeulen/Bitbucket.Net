using System.Threading.Tasks;
using Bitbucket.Net.Models.Core.Tasks;
using Flurl.Http;

namespace Bitbucket.Net
{
    public partial class BitbucketClient
    {
        private IFlurlRequest GetTasksUrl() => GetBaseUrl()
            .AppendPathSegment("/tasks");

        private IFlurlRequest GetTasksUrl(string path) => GetTasksUrl()
            .AppendPathSegment(path);

        public async Task<BitbucketTask> CreateTaskAsync(TaskInfo taskInfo)
        {
            var response = await GetTasksUrl()
                .PostJsonAsync(taskInfo)
                .ConfigureAwait(false);

            return await HandleResponseAsync<BitbucketTask>(response).ConfigureAwait(false);
        }

        public async Task<BitbucketTask> GetTaskAsync(long taskId)
        {
            return await GetTasksUrl($"/{taskId}")
                .GetJsonAsync<BitbucketTask>()
                .ConfigureAwait(false);
        }

        public async Task<BitbucketTask> UpdateTaskAsync(long taskId, string text)
        {
            var obj = new
            {
                id = taskId,
                text
            };

            var response = await GetTasksUrl($"/{taskId}")
                .PutJsonAsync(obj)
                .ConfigureAwait(false);

            return await HandleResponseAsync<BitbucketTask>(response).ConfigureAwait(false);
        }

        public async Task<bool> DeleteTaskAsync(long taskId)
        {
            var response = await GetTasksUrl($"/{taskId}")
                .DeleteAsync()
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }
    }
}
