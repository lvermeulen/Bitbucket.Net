using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Bitbucket.Net.Common;
using Bitbucket.Net.Common.Models;
using Bitbucket.Net.Models.Core.Admin;
using Bitbucket.Net.Models.Core.Projects;
using Bitbucket.Net.Models.Core.Tasks;
using Bitbucket.Net.Models.Core.Users;
using Flurl.Http;
using Newtonsoft.Json;
using NullValueHandling = Flurl.NullValueHandling;

namespace Bitbucket.Net
{
    public partial class BitbucketClient
    {
        private IFlurlRequest GetProjectsUrl() => GetBaseUrl()
            .AppendPathSegment("/projects");

        private IFlurlRequest GetProjectsUrl(string path) => GetProjectsUrl()
            .AppendPathSegment(path);

        private IFlurlRequest GetProjectUrl(string projectKey) => GetProjectsUrl()
            .AppendPathSegment($"/{projectKey}");

        private IFlurlRequest GetProjectsReposUrl(string projectKey, string repositorySlug) => GetProjectsUrl($"/{projectKey}/repos/{repositorySlug}");

        private IFlurlRequest GetProjectsReposUrl(string projectKey, string repositorySlug, string path) => GetProjectsReposUrl(projectKey, repositorySlug)
            .AppendPathSegment(path);

        public async Task<IEnumerable<Project>> GetProjectsAsync(
            int? maxPages = null,
            int? limit = null,
            int? start = null,
            string name = null,
            Permissions? permission = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start,
                ["name"] = name,
                ["permission"] = BitbucketHelpers.PermissionToString(permission)
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                await GetProjectsUrl()
                    .SetQueryParams(qpv)
                    .GetJsonAsync<PagedResults<Project>>()
                    .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<Project> CreateProjectAsync(ProjectDefinition projectDefinition)
        {
            var response = await GetProjectsUrl()
                .PostJsonAsync(projectDefinition)
                .ConfigureAwait(false);

            return await HandleResponseAsync<Project>(response).ConfigureAwait(false);
        }

        public async Task<bool> DeleteProjectAsync(string projectKey)
        {
            var response = await GetProjectsUrl($"/{projectKey}")
                .DeleteAsync()
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<Project> UpdateProjectAsync(string projectKey, ProjectDefinition projectDefinition)
        {
            var response = await GetProjectsUrl($"/{projectKey}")
                .PutJsonAsync(projectDefinition)
                .ConfigureAwait(false);

            return await HandleResponseAsync<Project>(response).ConfigureAwait(false);
        }

        public async Task<Project> GetProjectAsync(string projectKey)
        {
            var response = await GetProjectsUrl($"/{projectKey}")
                .GetJsonAsync()
                .ConfigureAwait(false);

            return await HandleResponseAsync<Project>(response).ConfigureAwait(false);
        }

        public async Task<IEnumerable<UserPermission>> GetProjectUserPermissionsAsync(string projectKey, string filter = null,
            int? maxPages = null,
            int? limit = null,
            int? start = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start,
                ["filter"] = filter
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                await GetProjectsUrl($"/{projectKey}/permissions/users")
                    .SetQueryParams(qpv)
                    .GetJsonAsync<PagedResults<UserPermission>>()
                    .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<bool> DeleteProjectUserPermissionsAsync(string projectKey, string userName)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["name"] = userName
            };

            var response = await GetProjectsUrl($"/{projectKey}/permissions/users")
                .SetQueryParams(queryParamValues)
                .DeleteAsync()
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<bool> UpdateProjectUserPermissionsAsync(string projectKey, string userName, Permissions permission)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["name"] = userName,
                ["permission"] = BitbucketHelpers.PermissionToString(permission)
            };

            var response = await GetProjectsUrl($"/{projectKey}/permissions/users")
                .SetQueryParams(queryParamValues)
                .PutAsync(new StringContent(""))
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<IEnumerable<LicensedUser>> GetProjectUserPermissionsNoneAsync(string projectKey, string filter = null,
            int? maxPages = null,
            int? limit = null,
            int? start = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start,
                ["filter"] = filter
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                    await GetProjectsUrl($"/{projectKey}/permissions/users/none")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<PagedResults<LicensedUser>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<GroupPermission>> GetProjectGroupPermissionsAsync(string projectKey, string filter = null,
            int? maxPages = null,
            int? limit = null,
            int? start = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start,
                ["filter"] = filter
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                await GetProjectsUrl($"/{projectKey}/permissions/groups")
                    .SetQueryParams(qpv)
                    .GetJsonAsync<PagedResults<GroupPermission>>()
                    .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<bool> DeleteProjectGroupPermissionsAsync(string projectKey, string groupName)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["name"] = groupName
            };

            var response = await GetProjectsUrl($"/{projectKey}/permissions/groups")
                .SetQueryParams(queryParamValues)
                .DeleteAsync()
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<bool> UpdateProjectGroupPermissionsAsync(string projectKey, string groupName, Permissions permission)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["name"] = groupName,
                ["permission"] = BitbucketHelpers.PermissionToString(permission)
            };

            var response = await GetProjectsUrl($"/{projectKey}/permissions/groups")
                .SetQueryParams(queryParamValues)
                .PutAsync(new StringContent(""))
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<IEnumerable<LicensedUser>> GetProjectGroupPermissionsNoneAsync(string projectKey, string filter = null,
            int? maxPages = null,
            int? limit = null,
            int? start = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start,
                ["filter"] = filter
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                    await GetProjectsUrl($"/{projectKey}/permissions/groups/none")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<PagedResults<LicensedUser>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<bool> IsProjectDefaultPermissionAsync(string projectKey, Permissions permission)
        {
            var response = await GetProjectsUrl($"/{projectKey}/permissions/{BitbucketHelpers.PermissionToString(permission)}/all")
                .GetAsync()
                .ConfigureAwait(false);

            return await HandleResponseAsync<bool>(response, s => 
                    BitbucketHelpers.StringToBool(JsonConvert.DeserializeObject<dynamic>(s).permitted.ToString()))
                .ConfigureAwait(false);
        }

        private async Task<bool> SetProjectDefaultPermissionAsync(string projectKey, Permissions permission, bool allow)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["allow"] = BitbucketHelpers.BoolToString(allow)
            };

            var response = await GetProjectsUrl($"/{projectKey}/permissions/{BitbucketHelpers.PermissionToString(permission)}/all")
                .SetQueryParams(queryParamValues)
                .PostJsonAsync(new StringContent(""))
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<bool> GrantProjectPermissionToAllAsync(string projectKey, Permissions permission)
        {
            return await SetProjectDefaultPermissionAsync(projectKey, permission, true);
        }

        public async Task<bool> RevokeProjectPermissionFromAllAsync(string projectKey, Permissions permission)
        {
            return await SetProjectDefaultPermissionAsync(projectKey, permission, false);
        }

        public async Task<IEnumerable<Repository>> GetProjectRepositoriesAsync(string projectKey,
            int? maxPages = null,
            int? limit = null,
            int? start = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                await GetProjectsUrl($"/{projectKey}/repos")
                    .SetQueryParams(qpv)
                    .GetJsonAsync<PagedResults<Repository>>()
                    .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<Repository> CreateProjectRepositoryAsync(string projectKey, string repositoryName, string scmId = "git")
        {
            var data = new
            {
                name = repositoryName,
                scmId
            };

            var response = await GetProjectUrl($"/{projectKey}/repos")
                .PostJsonAsync(data)
                .ConfigureAwait(false);

            return await HandleResponseAsync<Repository>(response).ConfigureAwait(false);
        }

        public async Task<Repository> GetProjectRepositoryAsync(string projectKey, string repositorySlug)
        {
            return await GetProjectsReposUrl(projectKey, repositorySlug)
                .GetJsonAsync<Repository>()
                .ConfigureAwait(false);
        }

        public async Task<RepositoryFork> CreateProjectRepositoryForkAsync(string projectKey, string repositorySlug, string targetProjectKey = null, string targetSlug = null, string targetName = null)
        {
            var data = new
            {
                slug = targetSlug ?? repositorySlug,
                name = targetName,
                project = new ProjectRef { Key = targetProjectKey }
            };

            var response = await GetProjectsReposUrl(projectKey, repositorySlug)
                .PostJsonAsync(data)
                .ConfigureAwait(false);

            return await HandleResponseAsync<RepositoryFork>(response).ConfigureAwait(false);
        }

        public async Task<bool> ScheduleProjectRepositoryForDeletionAsync(string projectKey, string repositorySlug)
        {
            var response = await GetProjectsReposUrl(projectKey, repositorySlug)
                .DeleteAsync()
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<Repository> UpdateProjectRepositoryAsync(string projectKey, string repositorySlug,
            string targetName = null,
            bool? isForkable = null,
            string targetProjectKey = null,
            bool? isPublic = null)
        {
            var data = new DynamicDictionary
            {
                { targetName, "name" },
                { isForkable, "forkable" },
                { targetProjectKey, "project", new ProjectRef { Key = targetProjectKey } },
                { isPublic, "public" }
            };

            var response = await GetProjectsReposUrl(projectKey, repositorySlug)
                .PutJsonAsync(data.ToDictionary())
                .ConfigureAwait(false);

            return await HandleResponseAsync<Repository>(response).ConfigureAwait(false);
        }

        public async Task<IEnumerable<RepositoryFork>> GetProjectRepositoryForksAsync(string projectKey, string repositorySlug,
            int? maxPages = null,
            int? limit = null,
            int? start = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                    await GetProjectsReposUrl(projectKey, repositorySlug, "/forks")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<PagedResults<RepositoryFork>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<Repository> RecreateProjectRepositoryAsync(string projectKey, string repositorySlug)
        {
            var response = await GetProjectsReposUrl(projectKey, repositorySlug, "/recreate")
                .PostJsonAsync(new StringContent(""))
                .ConfigureAwait(false);

            return await HandleResponseAsync<Repository>(response).ConfigureAwait(false);
        }

        public async Task<IEnumerable<RepositoryFork>> GetRelatedProjectRepositoriesAsync(string projectKey, string repositorySlug,
            int? maxPages = null,
            int? limit = null,
            int? start = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                    await GetProjectsReposUrl(projectKey, repositorySlug, "/related")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<PagedResults<RepositoryFork>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<byte[]> GetProjectRepositoryArchiveAsync(string projectKey, string repositorySlug, 
            string at,
            string fileName,
            ArchiveFormats archiveFormat,
            string path,
            string prefix)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["at"] = at,
                ["fileName"] = fileName,
                ["format"] = BitbucketHelpers.ArchiveFormatToString(archiveFormat),
                ["path"] = path,
                ["prefix"] = prefix
            };

            return await GetProjectsReposUrl(projectKey, repositorySlug, "/archive")
                .SetQueryParams(queryParamValues)
                .GetBytesAsync()
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<GroupPermission>> GetProjectRepositoryGroupPermissionsAsync(string projectKey, string repositorySlug,
            string filter = null,
            int? maxPages = null,
            int? limit = null,
            int? start = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["filter"] = filter,
                ["limit"] = limit,
                ["start"] = start
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                    await GetProjectsReposUrl(projectKey, repositorySlug, "/permissions/groups")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<PagedResults<GroupPermission>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<bool> UpdateProjectRepositoryGroupPermissionsAsync(string projectKey, string repositorySlug, Permissions permission, string name)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["permission"] = permission,
                ["name"] = name
            };

            var response = await GetProjectsReposUrl(projectKey, repositorySlug, "/permissions/groups")
                .SetQueryParams(queryParamValues)
                .PutJsonAsync(new StringContent(""))
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<bool> DeleteProjectRepositoryGroupPermissionsAsync(string projectKey, string repositorySlug, string name)
        {
            var response = await GetProjectsReposUrl(projectKey, repositorySlug, "/permissions/groups")
                .SetQueryParam("name", name)
                .DeleteAsync()
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<IEnumerable<DeletableGroupOrUser>> GetProjectRepositoryGroupPermissionsNoneAsync(string projectKey, string repositorySlug, 
            string filter = null,
            int? maxPages = null,
            int? limit = null,
            int? start = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start,
                ["filter"] = filter
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                    await GetProjectsReposUrl(projectKey, repositorySlug, "/permissions/groups/none")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<PagedResults<DeletableGroupOrUser>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<UserPermission>> GetProjectRepositoryUserPermissionsAsync(string projectKey, string repositorySlug,
            string filter = null,
            int? maxPages = null,
            int? limit = null,
            int? start = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["filter"] = filter,
                ["limit"] = limit,
                ["start"] = start
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                    await GetProjectsReposUrl(projectKey, repositorySlug, "/permissions/users")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<PagedResults<UserPermission>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<bool> UpdateProjectRepositoryUserPermissionsAsync(string projectKey, string repositorySlug, Permissions permission, string name)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["permission"] = permission,
                ["name"] = name
            };

            var response = await GetProjectsReposUrl(projectKey, repositorySlug, "/permissions/users")
                .SetQueryParams(queryParamValues)
                .PutJsonAsync(new StringContent(""))
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<bool> DeleteProjectRepositoryUserPermissionsAsync(string projectKey, string repositorySlug, string name)
        {
            var response = await GetProjectsReposUrl(projectKey, repositorySlug, "/permissions/users")
                .SetQueryParam("name", name)
                .DeleteAsync()
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<IEnumerable<User>> GetProjectRepositoryUserPermissionsNoneAsync(string projectKey, string repositorySlug, 
            string filter = null,
            int? maxPages = null,
            int? limit = null,
            int? start = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start,
                ["filter"] = filter
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                    await GetProjectsReposUrl(projectKey, repositorySlug, "/permissions/users/none")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<PagedResults<User>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<Branch>> GetBranchesAsync(string projectKey, string repositorySlug,
            int? maxPages = null,
            int? limit = null,
            int? start = null,
            string baseBranchOrTag = null,
            bool? details = null,
            string filterText = null,
            BranchOrderBy? orderBy = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start,
                ["base"] = baseBranchOrTag,
                ["details"] = details.HasValue ? BitbucketHelpers.BoolToString(details.Value) : null,
                ["filterText"] = filterText,
                ["orderBy"] = orderBy.HasValue ? BitbucketHelpers.BranchOrderByToString(orderBy.Value) : null
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                await GetProjectsReposUrl(projectKey, repositorySlug, "/branches")
                    .SetQueryParams(qpv)
                    .GetJsonAsync<PagedResults<Branch>>()
                    .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<Branch> CreateBranchAsync(string projectKey, string repositorySlug, BranchInfo branchInfo)
        {
            var response = await GetProjectsReposUrl(projectKey, repositorySlug, "/branches")
                .PostJsonAsync(branchInfo)
                .ConfigureAwait(false);

            return await HandleResponseAsync<Branch>(response).ConfigureAwait(false);
        }

        public async Task<Branch> GetDefaultBranchAsync(string projectKey, string repositorySlug)
        {
            return await GetProjectsReposUrl(projectKey, repositorySlug, "/branches/default")
                .GetJsonAsync<Branch>()
                .ConfigureAwait(false);
        }

        public async Task<bool> SetDefaultBranchAsync(string projectKey, string repositorySlug, BranchRef branchRef)
        {
            var response = await GetProjectsReposUrl(projectKey, repositorySlug, "/branches")
                .PutJsonAsync(branchRef)
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<BrowseItem> BrowseProjectRepositoryAsync(string projectKey, string repositorySlug, string at, bool type = false,
            bool blame = false,
            bool noContent = false)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["at"] = at,
                ["type"] = BitbucketHelpers.BoolToString(type),
            };
            if (blame)
            {
                queryParamValues.Add("blame", null);
            }
            if (blame && noContent)
            {
                queryParamValues.Add("noContent", null);
            }

            return await GetProjectsReposUrl(projectKey, repositorySlug, "/browse")
                .SetQueryParams(queryParamValues, NullValueHandling.NameOnly)
                .GetJsonAsync<BrowseItem>()
                .ConfigureAwait(false);
        }

        public async Task<BrowsePathItem> BrowseProjectRepositoryPathAsync(string projectKey, string repositorySlug, string path, string at, bool type = false,
            bool blame = false,
            bool noContent = false)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["at"] = at,
                ["type"] = BitbucketHelpers.BoolToString(type),
            };
            if (blame)
            {
                queryParamValues.Add("blame", null);
            }
            if (blame && noContent)
            {
                queryParamValues.Add("noContent", null);
            }

            return await GetProjectsReposUrl(projectKey, repositorySlug, $"/browse/{path}")
                .SetQueryParams(queryParamValues, NullValueHandling.NameOnly)
                .GetJsonAsync<BrowsePathItem>()
                .ConfigureAwait(false);
        }

        public async Task<Commit> UpdateProjectRepositoryPathAsync(string projectKey, string repositorySlug, string path,
            string fileName,
            string branch,
            string message = null,
            string sourceCommitId = null,
            string sourceBranch = null)
        {
            if (!File.Exists(fileName))
            {
                throw new ArgumentException($"File doesn't exist: {fileName}");
            }

            long fileSize = new FileInfo(path).Length;
            var buffer = new byte[fileSize];
            using (var stm = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Read))
            {
                await stm.ReadAsync(buffer, 0, (int)fileSize);
                var memoryStream = new MemoryStream(buffer);

                var data = new DynamicMultipartFormDataContent
                {
                    { new StreamContent(memoryStream), "content" },
                    { new StringContent(branch), "branch" },
                    { message, new StringContent(message), "message" },
                    { sourceCommitId, new StringContent(sourceCommitId), "sourceCommitId" },
                    { sourceBranch, new StringContent(sourceBranch), "sourceBranch" }
                };

                var response = await GetProjectsReposUrl(projectKey, repositorySlug, $"/browse/{path}")
                    .PutAsync(data.ToMultipartFormDataContent())
                    .ConfigureAwait(false);

                return await HandleResponseAsync<Commit>(response).ConfigureAwait(false);
            }
        }

        public async Task<IEnumerable<Change>> GetChangesAsync(string projectKey, string repositorySlug, string until, string since = null,
            int? maxPages = null,
            int? limit = null,
            int? start = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start,
                ["since"] = since,
                ["until"] = until
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                    await GetProjectsReposUrl(projectKey, repositorySlug, "/changes")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<PagedResults<Change>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<Commit>> GetCommitsAsync(string projectKey, string repositorySlug, 
            string until, 
            bool followRenames = false, 
            bool ignoreMissing = false, 
            MergeCommits merges = MergeCommits.Exclude,
            string path = null, 
            string since = null, 
            bool withCounts = false,
            int? maxPages = null,
            int? limit = null,
            int? start = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start,
                ["followRenames"] = BitbucketHelpers.BoolToString(followRenames),
                ["ignoreMissing"] = BitbucketHelpers.BoolToString(ignoreMissing),
                ["merges"] = BitbucketHelpers.MergeCommitsToString(merges),
                ["path"] = path,
                ["since"] = since,
                ["until"] = until,
                ["withCounts"] = BitbucketHelpers.BoolToString(withCounts),
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                    await GetProjectsReposUrl(projectKey, repositorySlug, "/commits")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<PagedResults<Commit>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<Commit> GetCommitAsync(string projectKey, string repositorySlug, string commitId, string path = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["path"] = path,
            };

            return await GetProjectsReposUrl(projectKey, repositorySlug, $"/commits/{commitId}")
                .SetQueryParams(queryParamValues)
                .GetJsonAsync<Commit>()
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<Change>> GetCommitChangesAsync(string projectKey, string repositorySlug, string commitId,
            string since = null,
            bool withComments = true,
            int? maxPages = null,
            int? limit = null,
            int? start = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start,
                ["since"] = since,
                ["withComments"] = BitbucketHelpers.BoolToString(withComments),
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                    await GetProjectsReposUrl(projectKey, repositorySlug, $"/commits/{commitId}/changes")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<PagedResults<Change>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<Comment>> GetCommitCommentsAsync(string projectKey, string repositorySlug, string commitId,
            string path,
            string since = null,
            int? maxPages = null,
            int? limit = null,
            int? start = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start,
                ["path"] = path,
                ["since"] = since
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                    await GetProjectsReposUrl(projectKey, repositorySlug, $"/commits/{commitId}/comments")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<PagedResults<Comment>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<CommentRef> CreateCommitCommentAsync(string projectKey, string repositorySlug, string commitId, 
            CommentInfo commentInfo, string since = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["since"] = since
            };

            var response = await GetProjectsReposUrl(projectKey, repositorySlug, $"/commits/{commitId}/comments")
                .SetQueryParams(queryParamValues)
                .PostJsonAsync(commentInfo)
                .ConfigureAwait(false);

            return await HandleResponseAsync<CommentRef>(response).ConfigureAwait(false);
        }

        public async Task<CommentRef> GetCommitCommentAsync(string projectKey, string repositorySlug, string commitId, long commentId)
        {
            return await GetProjectsReposUrl(projectKey, repositorySlug, $"/commits/{commitId}/comments/{commentId}")
                .GetJsonAsync<CommentRef>()
                .ConfigureAwait(false);
        }

        public async Task<CommentRef> UpdateCommitCommentAsync(string projectKey, string repositorySlug, string commitId, long commentId, 
            CommentText commentText)
        {
            var response = await GetProjectsReposUrl(projectKey, repositorySlug, $"/commits/{commitId}/comments/{commentId}")
                .PutJsonAsync(commentText)
                .ConfigureAwait(false);

            return await HandleResponseAsync<CommentRef>(response).ConfigureAwait(false);
        }

        public async Task<bool> DeleteCommitCommentAsync(string projectKey, string repositorySlug, string commitId, long commentId,
            int version = -1)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["version"] = version
            };

            var response = await GetProjectsReposUrl(projectKey, repositorySlug, $"/commits/{commitId}/comments/{commentId}")
                .SetQueryParams(queryParamValues)
                .DeleteAsync()
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<Differences> GetCommitDiffAsync(string projectKey, string repositorySlug, string commitId,
            bool autoSrcPath = false,
            int contextLines = -1,
            string since = null,
            string srcPath = null,
            string whitespace = "ignore-all",
            bool withComments = true)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["autoSrcPath"] = BitbucketHelpers.BoolToString(autoSrcPath),
                ["contextLines"] = contextLines,
                ["since"] = since,
                ["srcPath"] = srcPath,
                ["whitespace"] = whitespace,
                ["withComments"] = BitbucketHelpers.BoolToString(withComments),
            };

            return await GetProjectsReposUrl(projectKey, repositorySlug, $"/commits/{commitId}/diff")
                .SetQueryParams(queryParamValues)
                .GetJsonAsync<Differences>()
                .ConfigureAwait(false);
        }

        public async Task<bool> CreateCommitWatchAsync(string projectKey, string repositorySlug, string commitId)
        {
            var response = await GetProjectsReposUrl(projectKey, repositorySlug, $"/commits/{commitId}/watch")
                .PostJsonAsync(new StringContent(""))
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<bool> DeleteCommitWatchAsync(string projectKey, string repositorySlug, string commitId)
        {
            var response = await GetProjectsReposUrl(projectKey, repositorySlug, $"/commits/{commitId}/watch")
                .DeleteAsync()
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Change>> GetRepositoryCompareChangesAsync(string projectKey, string repositorySlug, string from, string to, 
            string fromRepo = null,
            int? maxPages = null,
            int? limit = null,
            int? start = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start,
                ["from"] = from,
                ["to"] = to,
                ["fromRepo"] = fromRepo
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                    await GetProjectsReposUrl(projectKey, repositorySlug, "/compare/changes")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<PagedResults<Change>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<Differences> GetRepositoryCompareDiffAsync(string projectKey, string repositorySlug, string from, string to,
            string fromRepo = null,
            string srcPath = null,
            int contextLines = -1,
            string whitespace = "ignore-all")
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["from"] = from,
                ["to"] = to,
                ["fromRepo"] = fromRepo,
                ["srcPath"] = srcPath,
                ["contextLines"] = contextLines,
                ["whitespace"] = whitespace,
            };

            return await GetProjectsReposUrl(projectKey, repositorySlug, "/compare/diff")
                .SetQueryParams(queryParamValues)
                .GetJsonAsync<Differences>()
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<Commit>> GetRepositoryCompareCommitsAsync(string projectKey, string repositorySlug, string from, string to,
            string fromRepo = null,
            int? maxPages = null,
            int? limit = null,
            int? start = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start,
                ["from"] = from,
                ["to"] = to,
                ["fromRepo"] = fromRepo
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                await GetProjectsReposUrl(projectKey, repositorySlug, "/compare/commits")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<PagedResults<Commit>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<Differences> GetRepositoryDiffAsync(string projectKey, string repositorySlug, string until,
            int contextLines = -1, 
            string since = null, 
            string srcPath = null,
            string whitespace = "ignore-all")
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["contextLines"] = contextLines,
                ["since"] = since,
                ["srcPath"] = srcPath,
                ["until"] = until,
                ["whitespace"] = whitespace
            };

            return await GetProjectsReposUrl(projectKey, repositorySlug, "/diff")
                .SetQueryParams(queryParamValues)
                .GetJsonAsync<Differences>()
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<string>> GetRepositoryFilesAsync(string projectKey, string repositorySlug, string at = null,
            int? maxPages = null,
            int? limit = null,
            int? start = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start,
                ["at"] = at
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                    await GetProjectsReposUrl(projectKey, repositorySlug, "/files")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<PagedResults<string>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<LastModified> GetProjectRepositoryLastModifiedAsync(string projectKey, string repositorySlug, string at)
        {
            return await GetProjectsReposUrl(projectKey, repositorySlug, "/last-modified")
                .SetQueryParam("at", at)
                .GetJsonAsync<LastModified>()
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<Identity>> GetRepositoryParticipantsAsync(string projectKey, string repositorySlug, 
            PullRequestDirections direction = PullRequestDirections.Incoming,
            string filter = null,
            Roles? role = null,
            int? maxPages = null,
            int? limit = null,
            int? start = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start,
                ["direction"] = BitbucketHelpers.PullRequestDirectionToString(direction),
                ["filter"] = filter,
                ["role"] = BitbucketHelpers.RoleToString(role)
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                    await GetProjectsReposUrl(projectKey, repositorySlug, "/participants")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<PagedResults<Identity>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<PullRequest>> GetPullRequestsAsync(string projectKey, string repositorySlug,
            int? maxPages = null,
            int? limit = null,
            int? start = null,
            PullRequestDirections direction = PullRequestDirections.Incoming,
            string branchId = null,
            PullRequestStates state = PullRequestStates.Open,
            PullRequestOrders order = PullRequestOrders.Newest,
            bool withAttributes = true,
            bool withProperties = true)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start,
                ["direction"] = BitbucketHelpers.PullRequestDirectionToString(direction),
                ["at"] = branchId,
                ["state"] = BitbucketHelpers.PullRequestStateToString(state),
                ["order"] = BitbucketHelpers.PullRequestOrderToString(order),
                ["withAttributes"] = BitbucketHelpers.BoolToString(withAttributes),
                ["withProperties"] = BitbucketHelpers.BoolToString(withProperties),
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                await GetProjectsReposUrl(projectKey, repositorySlug, "/pull-requests")
                    .SetQueryParams(qpv)
                    .GetJsonAsync<PagedResults<PullRequest>>()
                    .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<PullRequest> CreatePullRequestAsync(string projectKey, string repositorySlug, PullRequestInfo pullRequestInfo)
        {
            var response = await GetProjectsReposUrl(projectKey, repositorySlug, "/pull-requests")
                .PostJsonAsync(pullRequestInfo)
                .ConfigureAwait(false);

            return await HandleResponseAsync<PullRequest>(response).ConfigureAwait(false);
        }

        public async Task<PullRequest> GetPullRequestAsync(string projectKey, string repositorySlug, long pullRequestId)
        {
            return await GetProjectsReposUrl(projectKey, repositorySlug, $"/pull-requests/{pullRequestId}")
                .GetJsonAsync<PullRequest>()
                .ConfigureAwait(false);
        }

        public async Task<PullRequest> UpdatePullRequestAsync(string projectKey, string repositorySlug, long pullRequestId, PullRequestUpdate pullRequestUpdate)
        {
            var response = await GetProjectsReposUrl(projectKey, repositorySlug, $"/pull-requests/{pullRequestId}")
                .PutJsonAsync(pullRequestUpdate)
                .ConfigureAwait(false);

            return await HandleResponseAsync<PullRequest>(response).ConfigureAwait(false);
        }

        public async Task<bool> DeletePullRequestAsync(string projectKey, string repositorySlug, long pullRequestId, VersionInfo versionInfo)
        {
            var response = await GetProjectsReposUrl(projectKey, repositorySlug, $"/pull-requests/{pullRequestId}")
                .SendJsonAsync(HttpMethod.Delete, versionInfo)
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<IEnumerable<PullRequestActivity>> GetPullRequestActivitiesAsync(string projectKey, string repositorySlug, long pullRequestId,
            long? fromId = null,
            PullRequestFromTypes? fromType = null,
            int? maxPages = null,
            int? limit = null,
            int? start = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start,
                ["fromId"] = fromId,
                ["fromType"] = BitbucketHelpers.PullRequestFromTypeToString(fromType)
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                    await GetProjectsReposUrl(projectKey, repositorySlug, $"/pull-requests/{pullRequestId}/activities")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<PagedResults<PullRequestActivity>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<bool> DeclinePullRequestAsync(string projectKey, string repositorySlug, long pullRequestId, int version = -1)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["version"] = version
            };

            var response = await GetProjectsReposUrl(projectKey, repositorySlug, $"/pull-requests/{pullRequestId}/decline")
                .SetQueryParams(queryParamValues)
                .PostJsonAsync(new StringContent(""))
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<PullRequestMergeState> GetPullRequestMergeStateAsync(string projectKey, string repositorySlug, long pullRequestId, int version = -1)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["version"] = version
            };

            return await GetProjectsReposUrl(projectKey, repositorySlug, $"/pull-requests/{pullRequestId}/merge")
                .SetQueryParams(queryParamValues)
                .GetJsonAsync<PullRequestMergeState>()
                .ConfigureAwait(false);
        }

        public async Task<PullRequest> MergePullRequestAsync(string projectKey, string repositorySlug, long pullRequestId, int version = -1)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["version"] = version
            };

            var response = await GetProjectsReposUrl(projectKey, repositorySlug, $"/pull-requests/{pullRequestId}/merge")
                .SetQueryParams(queryParamValues)
                .PostJsonAsync(new StringContent(""))
                .ConfigureAwait(false);

            return await HandleResponseAsync<PullRequest>(response).ConfigureAwait(false);
        }

        public async Task<PullRequest> ReopenPullRequestAsync(string projectKey, string repositorySlug, long pullRequestId, int version = -1)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["version"] = version
            };

            var response = await GetProjectsReposUrl(projectKey, repositorySlug, $"/pull-requests/{pullRequestId}/reopen")
                .SetQueryParams(queryParamValues)
                .PostJsonAsync(new StringContent(""))
                .ConfigureAwait(false);

            return await HandleResponseAsync<PullRequest>(response).ConfigureAwait(false);
        }

        public async Task<Reviewer> ApprovePullRequestAsync(string projectKey, string repositorySlug, long pullRequestId)
        {
            var response = await GetProjectsReposUrl(projectKey, repositorySlug, $"/pull-requests/{pullRequestId}/approve")
                .PostJsonAsync(new StringContent(""))
                .ConfigureAwait(false);

            return await HandleResponseAsync<Reviewer>(response).ConfigureAwait(false);
        }

        public async Task<Reviewer> DeletePullRequestApprovalAsync(string projectKey, string repositorySlug, long pullRequestId)
        {
            var response = await GetProjectsReposUrl(projectKey, repositorySlug, $"/pull-requests/{pullRequestId}/approve")
                .DeleteAsync()
                .ConfigureAwait(false);

            return await HandleResponseAsync<Reviewer>(response).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Change>> GetPullRequestChangesAsync(string projectKey, string repositorySlug, long pullRequestId,
            ChangeScopes changeScope = ChangeScopes.All,
            string sinceId = null,
            string untilId = null,
            bool withComments = true,
            int? maxPages = null,
            int? limit = null,
            int? start = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start,
                ["changeScope"] = BitbucketHelpers.ChangeScopeToString(changeScope),
                ["sinceId"] = sinceId,
                ["untilId"] = untilId,
                ["withComments"] = BitbucketHelpers.BoolToString(withComments)
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                    await GetProjectsReposUrl(projectKey, repositorySlug, $"/pull-requests/{pullRequestId}/changes")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<PagedResults<Change>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<CommentRef> CreatePullRequestCommentAsync(string projectKey, string repositorySlug, long pullRequestId, 
            string text, 
            string parentId = null,
            DiffTypes? diffType = null,
            string fromHash = null,
            string path = null,
            string srcPath = null,
            string toHash = null,
            int? line = null,
            FileTypes? fileType = null,
            LineTypes? lineType = null)
        {
            var data = new
            {
                text,
                parent = new { id = parentId },
                diffType = BitbucketHelpers.DiffTypeToString(diffType),
                fromHash,
                path,
                srcPath,
                toHash,
                line,
                fileType = BitbucketHelpers.FileTypeToString(fileType),
                lineType = BitbucketHelpers.LineTypeToString(lineType)
            };

            var response = await GetProjectsReposUrl(projectKey, repositorySlug)
                .AppendPathSegment($"/pull-requests/{pullRequestId}/comments")
                .PostJsonAsync(data)
                .ConfigureAwait(false);

            return await HandleResponseAsync<CommentRef>(response).ConfigureAwait(false);
        }

        public async Task<IEnumerable<CommentRef>> GetPullRequestCommentsAsync(string projectKey, string repositorySlug, long pullRequestId,
            string path,
            AnchorStates anchorState = AnchorStates.Active,
            DiffTypes diffType = DiffTypes.Effective,
            string fromHash = null,
            string toHash = null,
            int? maxPages = null,
            int? limit = null,
            int? start = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start,
                ["path"] = path,
                ["anchorState"] = BitbucketHelpers.AnchorStateToString(anchorState),
                ["diffType"] = BitbucketHelpers.DiffTypeToString(diffType),
                ["fromHash"] = fromHash,
                ["toHash"] = toHash
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                    await GetProjectsReposUrl(projectKey, repositorySlug, $"/pull-requests/{pullRequestId}/comments")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<PagedResults<CommentRef>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<CommentRef> GetPullRequestCommentAsync(string projectKey, string repositorySlug, long pullRequestId, long commentId)
        {
            return await GetProjectsReposUrl(projectKey, repositorySlug)
                .AppendPathSegment($"/pull-requests/{pullRequestId}/comments/{commentId}")
                .GetJsonAsync<CommentRef>()
                .ConfigureAwait(false);
        }

        public async Task<CommentRef> UpdatePullRequestCommentAsync(string projectKey, string repositorySlug, long pullRequestId, long commentId,
            int version, string text)
        {
            var data = new
            {
                version,
                text
            };

            var response = await GetProjectsReposUrl(projectKey, repositorySlug)
                .AppendPathSegment($"/pull-requests/{pullRequestId}/comments/{commentId}")
                .SetQueryParam("version", version)
                .PutJsonAsync(data)
                .ConfigureAwait(false);

            return await HandleResponseAsync<CommentRef>(response).ConfigureAwait(false);
        }

        public async Task<bool> DeletePullRequestCommentAsync(string projectKey, string repositorySlug, long pullRequestId, long commentId,
            int version = -1)
        {
            var response = await GetProjectsReposUrl(projectKey, repositorySlug)
                .AppendPathSegment($"/pull-requests/{pullRequestId}/comments/{commentId}")
                .SetQueryParam("version", version)
                .DeleteAsync()
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Commit>> GetPullRequestCommitsAsync(string projectKey, string repositorySlug, long pullRequestId,
            bool withCounts = false,
            int? maxPages = null,
            int? limit = null,
            int? start = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start,
                ["withCounts"] = BitbucketHelpers.BoolToString(withCounts)
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                    await GetProjectsReposUrl(projectKey, repositorySlug, $"/pull-requests/{pullRequestId}/commits")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<PagedResults<Commit>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<Differences> GetPullRequestDiffAsync(string projectKey, string repositorySlug, long pullRequestId,
            int contextLines = -1,
            DiffTypes diffType = DiffTypes.Effective,
            string sinceId = null,
            string srcPath = null,
            string untilId = null,
            string whitespace = "ignore-all",
            bool withComments = true)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["contextLines"] = contextLines,
                ["diffType"] = BitbucketHelpers.DiffTypeToString(diffType),
                ["sinceId"] = sinceId,
                ["srcPath"] = srcPath,
                ["untilId"] = untilId,
                ["whitespace"] = whitespace,
                ["withComments"] = BitbucketHelpers.BoolToString(withComments)
            };

            return await GetProjectsReposUrl(projectKey, repositorySlug)
                .AppendPathSegment($"/pull-requests/{pullRequestId}/diff")
                .SetQueryParams(queryParamValues)
                .GetJsonAsync<Differences>()
                .ConfigureAwait(false);
        }

        public async Task<Differences> GetPullRequestDiffPathAsync(string projectKey, string repositorySlug, long pullRequestId,
            string path,
            int contextLines = -1,
            DiffTypes diffType = DiffTypes.Effective,
            string sinceId = null,
            string srcPath = null,
            string untilId = null,
            string whitespace = "ignore-all",
            bool withComments = true)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["contextLines"] = contextLines,
                ["diffType"] = BitbucketHelpers.DiffTypeToString(diffType),
                ["sinceId"] = sinceId,
                ["srcPath"] = srcPath,
                ["untilId"] = untilId,
                ["whitespace"] = whitespace,
                ["withComments"] = BitbucketHelpers.BoolToString(withComments)
            };

            return await GetProjectsReposUrl(projectKey, repositorySlug)
                .AppendPathSegment($"/pull-requests/{pullRequestId}/diff/{path}")
                .SetQueryParams(queryParamValues)
                .GetJsonAsync<Differences>()
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<Participant>> GetPullRequestParticipantsAsync(string projectKey, string repositorySlug, long pullRequestId,
            int? maxPages = null,
            int? limit = null,
            int? start = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                    await GetProjectsReposUrl(projectKey, repositorySlug)
                        .AppendPathSegment($"/pull-requests/{pullRequestId}/participants")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<PagedResults<Participant>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<Participant> AssignUserRoleToPullRequestAsync(string projectKey, string repositorySlug, long pullRequestId,
            Named named,
            Roles role)
        {
            var data = new
            {
                user = named,
                role = BitbucketHelpers.RoleToString(role)
            };

            var response = await GetProjectsReposUrl(projectKey, repositorySlug)
                .AppendPathSegment($"/pull-requests/{pullRequestId}/participants")
                .PostJsonAsync(data)
                .ConfigureAwait(false);

            return await HandleResponseAsync<Participant>(response).ConfigureAwait(false);
        }

        public async Task<bool> DeletePullRequestParticipantAsync(string projectKey, string repositorySlug, long pullRequestId, string userName)
        {
            var response = await GetProjectsReposUrl(projectKey, repositorySlug)
                .AppendPathSegment($"/pull-requests/{pullRequestId}/participants")
                .SetQueryParam("username", userName)
                .DeleteAsync()
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<Participant> UpdatePullRequestParticipantStatus(string projectKey, string repositorySlug, long pullRequestId,
            string userSlug,
            Named named,
            bool approved,
            ParticipantStatus participantStatus)
        {
            var data = new
            {
                user = named,
                approved = BitbucketHelpers.BoolToString(approved),
                status = BitbucketHelpers.ParticipantStatusToString(participantStatus)
            };

            var response = await GetProjectsReposUrl(projectKey, repositorySlug)
                .AppendPathSegment($"/pull-requests/{pullRequestId}/participants/{userSlug}")
                .PutJsonAsync(data)
                .ConfigureAwait(false);

            return await HandleResponseAsync<Participant>(response).ConfigureAwait(false);
        }

        public async Task<bool> UnassignUserFromPullRequestAsync(string projectKey, string repositorySlug, long pullRequestId, string userSlug)
        {
            var response = await GetProjectsReposUrl(projectKey, repositorySlug)
                .AppendPathSegment($"/pull-requests/{pullRequestId}/participants/{userSlug}")
                .DeleteAsync()
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<IEnumerable<BitbucketTask>> GetPullRequestTasksAsync(string projectKey, string repositorySlug, long pullRequestId,
            int? maxPages = null,
            int? limit = null,
            int? start = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                    await GetProjectsReposUrl(projectKey, repositorySlug, $"/pull-requests/{pullRequestId}/tasks")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<PagedResults<BitbucketTask>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<BitbucketTaskCount> GetPullRequestTaskCountAsync(string projectKey, string repositorySlug, long pullRequestId)
        {
            return await GetProjectsReposUrl(projectKey, repositorySlug)
                .AppendPathSegment($"/pull-requests/{pullRequestId}/tasks/count")
                .GetJsonAsync<BitbucketTaskCount>()
                .ConfigureAwait(false);
        }

        public async Task<bool> WatchPullRequestAsync(string projectKey, string repositorySlug, long pullRequestId)
        {
            var response = await GetProjectsReposUrl(projectKey, repositorySlug)
                .AppendPathSegment($"/pull-requests/{pullRequestId}/watch")
                .PostJsonAsync(new StringContent(""))
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<bool> UnwatchPullRequestAsync(string projectKey, string repositorySlug, long pullRequestId)
        {
            var response = await GetProjectsReposUrl(projectKey, repositorySlug)
                .AppendPathSegment($"/pull-requests/{pullRequestId}/watch")
                .DeleteAsync()
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<Stream> RetrieveRawContentAsync(string projectKey, string repositorySlug, string path,
            string at = null,
            bool markup = false,
            bool hardWrap = true,
            bool htmlEscape = true)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["at"] = at,
                ["markup"] = BitbucketHelpers.BoolToString(markup),
                ["hardWrap"] = BitbucketHelpers.BoolToString(hardWrap),
                ["htmlEscape"] = BitbucketHelpers.BoolToString(htmlEscape)
            };

            return await GetProjectsReposUrl(projectKey, repositorySlug)
                .AppendPathSegment($"/raw/{path}")
                .SetQueryParams(queryParamValues)
                .GetStreamAsync()
                .ConfigureAwait(false);
        }

        public async Task<PullRequestSettings> GetProjectRepositoryPullRequestSettingsAsync(string projectKey, string repositorySlug)
        {
            return await GetProjectsReposUrl(projectKey, repositorySlug, "/settings/pull-requests")
                .GetJsonAsync<PullRequestSettings>()
                .ConfigureAwait(false);
        }

        public async Task<PullRequestSettings> UpdateProjectRepositoryPullRequestSettingsAsync(string projectKey, string repositorySlug,
            PullRequestSettings pullRequestSettings)
        {
            var response = await GetProjectsReposUrl(projectKey, repositorySlug, "/settings/pull-requests")
                .PostJsonAsync(pullRequestSettings)
                .ConfigureAwait(false);

            return await HandleResponseAsync<PullRequestSettings>(response).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Hook>> GetProjectRepositoryHooksSettingsAsync(string projectKey, string repositorySlug, 
            HookTypes? hookType = null,
            int? maxPages = null,
            int? limit = null,
            int? start = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start,
                ["type"] = hookType
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                    await GetProjectsReposUrl(projectKey, repositorySlug, "/settings/hooks")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<PagedResults<Hook>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<Hook> GetProjectRepositoryHookSettingsAsync(string projectKey, string repositorySlug, string hookKey)
        {
            return await GetProjectsReposUrl(projectKey, repositorySlug, $"/settings/hooks/{hookKey}")
                .GetJsonAsync<Hook>()
                .ConfigureAwait(false);
        }

        public async Task<bool> DeleteProjectRepositoryHookSettingsAsync(string projectKey, string repositorySlug, string hookKey)
        {
            var response = await GetProjectsReposUrl(projectKey, repositorySlug, $"/settings/hooks/{hookKey}")
                .DeleteAsync()
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        public async Task<Hook> EnableProjectRepositoryHookAsync(string projectKey, string repositorySlug, string hookKey, object hookSettings = null)
        {
            var response = await GetProjectsReposUrl(projectKey, repositorySlug, $"/settings/hooks/{hookKey}/enabled")
                .PutJsonAsync(hookSettings)
                .ConfigureAwait(false);

            return await HandleResponseAsync<Hook>(response).ConfigureAwait(false);
        }

        public async Task<Hook> DisableProjectRepositoryHookAsync(string projectKey, string repositorySlug, string hookKey)
        {
            var response = await GetProjectsReposUrl(projectKey, repositorySlug, $"/settings/hooks/{hookKey}/enabled")
                .DeleteAsync()
                .ConfigureAwait(false);

            return await HandleResponseAsync<Hook>(response).ConfigureAwait(false);
        }

        public async Task<Dictionary<string, object>> GetProjectRepositoryHookAllSettingsAsync(string projectKey, string repositorySlug, string hookKey)
        {
            return await GetProjectsReposUrl(projectKey, repositorySlug, $"/settings/hooks/{hookKey}/settings")
                .GetJsonAsync<Dictionary<string, object>>()
                .ConfigureAwait(false);
        }

        public async Task<Dictionary<string, object>> UpdateProjectRepositoryHookAllSettingsAsync(string projectKey, string repositorySlug, string hookKey,
            Dictionary<string, object> allSettings)
        {
            var response = await GetProjectsReposUrl(projectKey, repositorySlug, $"/settings/hooks/{hookKey}/settings")
                .PutJsonAsync(allSettings)
                .ConfigureAwait(false);

            return await HandleResponseAsync<Dictionary<string, object>>(response).ConfigureAwait(false);
        }

        public async Task<PullRequestSettings> GetProjectPullRequestsMergeStrategiesAsync(string projectKey, string scmId)
        {
            return await GetProjectUrl(projectKey)
                .AppendPathSegment($"/settings/pull-requests/{scmId}")
                .GetJsonAsync<PullRequestSettings>()
                .ConfigureAwait(false);
        }

        public async Task<MergeStrategies> UpdateProjectPullRequestsMergeStrategiesAsync(string projectKey, string scmId, MergeStrategies mergeStrategies)
        {
            var response = await GetProjectUrl(projectKey)
                .AppendPathSegment($"/settings/pull-requests/{scmId}")
                .PostJsonAsync(mergeStrategies)
                .ConfigureAwait(false);

            return await HandleResponseAsync<MergeStrategies>(response).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Tag>> GetProjectRepositoryTagsAsync(string projectKey, string repositorySlug,
            string filterText,
            BranchOrderBy orderBy,
            int? maxPages = null,
            int? limit = null,
            int? start = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start,
                ["filterText"] = filterText,
                ["orderBy"] = BitbucketHelpers.BranchOrderByToString(orderBy)
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                    await GetProjectsReposUrl(projectKey, repositorySlug, "/tags")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<PagedResults<Tag>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<Tag> CreateProjectRepositoryTagAsync(string projectKey, string repositorySlug,
            string name,
            string startPoint,
            string message)
        {
            var data = new DynamicDictionary
            {
                { name, "name" },
                { startPoint, "startPoint" },
                { message, "message" }
            };

            var response = await GetProjectsReposUrl(projectKey, repositorySlug, "/tags")
                .PostJsonAsync(data.ToDictionary())
                .ConfigureAwait(false);

            return await HandleResponseAsync<Tag>(response).ConfigureAwait(false);
        }

        public async Task<Tag> GetProjectRepositoryTagAsync(string projectKey, string repositorySlug, string tagName)
        {
            return await GetProjectsReposUrl(projectKey, repositorySlug, $"/tags/{tagName}")
                .GetJsonAsync<Tag>()
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<WebHook>> GetProjectRepositoryWebHooksAsync(string projectKey, string repositorySlug,
            string @event = null,
            bool statistics = false,
            int? maxPages = null,
            int? limit = null,
            int? start = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["start"] = start,
                ["event"] = @event,
                ["statistics"] = BitbucketHelpers.BoolToString(statistics)
            };

            return await GetPagedResultsAsync(maxPages, queryParamValues, async qpv =>
                    await GetProjectsReposUrl(projectKey, repositorySlug, "/webhooks")
                        .SetQueryParams(qpv)
                        .GetJsonAsync<PagedResults<WebHook>>()
                        .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<WebHook> CreateProjectRepositoryWebHookAsync(string projectKey, string repositorySlug, WebHook webHook)
        {
            var response = await GetProjectsReposUrl(projectKey, repositorySlug, "/webhooks")
                .PostJsonAsync(webHook)
                .ConfigureAwait(false);

            return await HandleResponseAsync<WebHook>(response).ConfigureAwait(false);
        }

        public async Task<WebHookTestRequestResponse> TestProjectRepositoryWebHookAsync(string projectKey, string repositorySlug, string url)
        {
            var response = await GetProjectsReposUrl(projectKey, repositorySlug, "/webhooks/test")
                .SetQueryParam("url", url)
                .PostJsonAsync(new StringContent(""))
                .ConfigureAwait(false);

            return await HandleResponseAsync<WebHookTestRequestResponse>(response).ConfigureAwait(false);
        }

        public async Task<WebHook> GetProjectRepositoryWebHookAsync(string projectKey, string repositorySlug,
            string webHookId,
            bool statistics = false)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["statistics"] = BitbucketHelpers.BoolToString(statistics)
            };

            return await GetProjectsReposUrl(projectKey, repositorySlug, $"/webhooks/{webHookId}")
                .SetQueryParams(queryParamValues)
                .GetJsonAsync<WebHook>()
                .ConfigureAwait(false);
        }

        public async Task<WebHook> UpdateProjectRepositoryWebHookAsync(string projectKey, string repositorySlug,
            string webHookId, WebHook webHook)
        {
            var response = await GetProjectsReposUrl(projectKey, repositorySlug, $"/webhooks/{webHookId}")
                .PutJsonAsync(webHook)
                .ConfigureAwait(false);

            return await HandleResponseAsync<WebHook>(response).ConfigureAwait(false);
        }

        public async Task<bool> DeleteProjectRepositoryWebHookAsync(string projectKey, string repositorySlug,
            string webHookId)
        {
            var response = await GetProjectsReposUrl(projectKey, repositorySlug, $"/webhooks/{webHookId}")
                .DeleteAsync()
                .ConfigureAwait(false);

            return await HandleResponseAsync(response).ConfigureAwait(false);
        }

        //public async Task<WebHookInvocation> GetProjectRepositoryWebHookLatestAsync(string projectKey, string repositorySlug,
        public async Task<string> GetProjectRepositoryWebHookLatestAsync(string projectKey, string repositorySlug,
            string webHookId,
            string @event = null,
            WebHookOutcomes? outcome = null)
        {
            var queryParamValues = new Dictionary<string, object>
            {
                ["event"] = @event,
                ["outcome"] = BitbucketHelpers.WebHookOutcomeToString(outcome)
            };

            return await GetProjectsReposUrl(projectKey, repositorySlug, $"/webhooks/{webHookId}/latest")
                .SetQueryParams(queryParamValues)
                //.GetJsonAsync<WebHookInvocation>()
                .GetStringAsync()
                .ConfigureAwait(false);
        }

        public async Task<WebHookStatistics> GetProjectRepositoryWebHookStatisticsAsync(string projectKey, string repositorySlug,
            string webHookId,
            string @event = null)
        {
            return await GetProjectsReposUrl(projectKey, repositorySlug, $"/webhooks/{webHookId}/statistics")
                .SetQueryParam("event", @event)
                .GetJsonAsync<WebHookStatistics>()
                .ConfigureAwait(false);
        }

        public async Task<Dictionary<string, WebHookStatisticsCounts>> GetProjectRepositoryWebHookStatisticsSummaryAsync(string projectKey, string repositorySlug,
            string webHookId)
        {
            return await GetProjectsReposUrl(projectKey, repositorySlug, $"/webhooks/{webHookId}/statistics/summary")
                .GetJsonAsync<Dictionary<string, WebHookStatisticsCounts>>()
                .ConfigureAwait(false);
        }
    }
}
