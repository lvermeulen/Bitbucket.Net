using System;
using System.Collections.Generic;
using Bitbucket.Net.Models.Projects;

namespace Bitbucket.Net.Common
{
    public static class BitbucketHelpers
    {
        private static readonly Dictionary<BranchOrderBy, string> s_stringByBranchOrderBy = new Dictionary<BranchOrderBy, string>
        {
            [BranchOrderBy.Alphabetical] = "ALPHABETICAL",
            [BranchOrderBy.Modification] = "MODIFICATION"
        };

        private static readonly Dictionary<PullRequestDirections, string> s_stringByPullRequestDirection = new Dictionary<PullRequestDirections, string>
        {
            [PullRequestDirections.Incoming] = "INCOMING",
            [PullRequestDirections.Outgoing] = "OUTGOING"
        };

        private static readonly Dictionary<PullRequestStates, string> s_stringByPullRequestState = new Dictionary<PullRequestStates, string>
        {
            [PullRequestStates.Open] = "OPEN",
            [PullRequestStates.Declined] = "DECLINED",
            [PullRequestStates.Merged] = "MERGED",
            [PullRequestStates.All] = "ALL"
        };

        private static readonly Dictionary<PullRequestOrders, string> s_stringByPullRequestOrder = new Dictionary<PullRequestOrders, string>
        {
            [PullRequestOrders.Newest] = "NEWEST",
            [PullRequestOrders.Oldest] = "OLDEST"
        };

        private static readonly Dictionary<Permissions, string> s_stringByPermission = new Dictionary<Permissions, string>
        {
            [Permissions.Admin] = "ADMIN",
            [Permissions.LicensedUser] = "LICENSED_USER",
            [Permissions.ProjectAdmin] = "PROJECT_ADMIN",
            [Permissions.ProjectCreate] = "PROJECT_CREATE",
            [Permissions.ProjectRead] = "PROJECT_READ",
            [Permissions.ProjectView] = "PROJECT_VIEW",
            [Permissions.ProjectWrite] = "PROJECT_WRITE",
            [Permissions.RepoAdmin] = "REPO_ADMIN",
            [Permissions.RepoRead] = "REPO_READ",
            [Permissions.RepoWrite] = "REPO_WRITE",
            [Permissions.SysAdmin] = "SYS_ADMIN",
        };

        private static readonly Dictionary<MergeCommits, string> s_stringByMergeCommits = new Dictionary<MergeCommits, string>
        {
            [MergeCommits.Exclude] = "exclude",
            [MergeCommits.Include] = "include",
            [MergeCommits.Only] = "only" 
        };

        public static string BoolToString(bool value) => value
            ? "true"
            : "false";

        public static bool StringToBool(string value) => value.Equals("true", StringComparison.OrdinalIgnoreCase);

        public static string BranchOrderByToString(BranchOrderBy orderBy)
        {
            if (!s_stringByBranchOrderBy.TryGetValue(orderBy, out string result))
            {
                throw new ArgumentException($"Unknown branch order by: {orderBy}");
            }

            return result;
        }

        public static string PullRequestDirectionToString(PullRequestDirections direction)
        {
            if (!s_stringByPullRequestDirection.TryGetValue(direction, out string result))
            {
                throw new ArgumentException($"Unknown pull request direction: {direction}");
            }

            return result;
        }

        public static string PullRequestStateToString(PullRequestStates state)
        {
            if (!s_stringByPullRequestState.TryGetValue(state, out string result))
            {
                throw new ArgumentException($"Unknown pull request state: {state}");
            }

            return result;
        }

        public static string PullRequestOrderToString(PullRequestOrders order)
        {
            if (!s_stringByPullRequestOrder.TryGetValue(order, out string result))
            {
                throw new ArgumentException($"Unknown pull request order: {order}");
            }

            return result;
        }

        public static string PermissionToString(Permissions permission)
        {
            if (!s_stringByPermission.TryGetValue(permission, out string result))
            {
                throw new ArgumentException($"Unknown permission: {permission}");
            }

            return result;
        }

        public static string PermissionToString(Permissions? permission)
        {
            return permission.HasValue 
                ? PermissionToString(permission.Value) 
                : null;
        }

        public static string MergeCommitsToString(MergeCommits mergeCommits)
        {
            if (!s_stringByMergeCommits.TryGetValue(mergeCommits, out string result))
            {
                throw new ArgumentException($"Unknown merge commit: {mergeCommits}");
            }

            return result;
        }
    }
}
