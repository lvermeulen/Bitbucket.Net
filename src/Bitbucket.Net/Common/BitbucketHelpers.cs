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

        private static readonly Dictionary<PullRequestDirection, string> s_stringByPullRequestDirection = new Dictionary<PullRequestDirection, string>
        {
            [PullRequestDirection.Incoming] = "INCOMING",
            [PullRequestDirection.Outgoing] = "OUTGOING"
        };

        private static readonly Dictionary<PullRequestState, string> s_stringByPullRequestState = new Dictionary<PullRequestState, string>
        {
            [PullRequestState.Open] = "OPEN",
            [PullRequestState.Declined] = "DECLINED",
            [PullRequestState.Merged] = "MERGED",
            [PullRequestState.All] = "ALL"
        };

        private static readonly Dictionary<PullRequestOrder, string> s_stringByPullRequestOrder = new Dictionary<PullRequestOrder, string>
        {
            [PullRequestOrder.Newest] = "NEWEST",
            [PullRequestOrder.Oldest] = "OLDEST"
        };

        public static string BoolToString(bool value) => value
            ? "true"
            : "false";

        public static string BranchOrderByToString(BranchOrderBy orderBy)
        {
            if (!s_stringByBranchOrderBy.TryGetValue(orderBy, out string result))
            {
                throw new ArgumentException($"Unknown branch order by: {orderBy}");
            }

            return result;
        }

        public static string PullRequestDirectionToString(PullRequestDirection direction)
        {
            if (!s_stringByPullRequestDirection.TryGetValue(direction, out string result))
            {
                throw new ArgumentException($"Unknown pull request direction: {direction}");
            }

            return result;
        }

        public static string PullRequestStateToString(PullRequestState state)
        {
            if (!s_stringByPullRequestState.TryGetValue(state, out string result))
            {
                throw new ArgumentException($"Unknown pull request state: {state}");
            }

            return result;
        }

        public static string PullRequestOrderToString(PullRequestOrder order)
        {
            if (!s_stringByPullRequestOrder.TryGetValue(order, out string result))
            {
                throw new ArgumentException($"Unknown pull request order: {order}");
            }

            return result;
        }
    }
}
