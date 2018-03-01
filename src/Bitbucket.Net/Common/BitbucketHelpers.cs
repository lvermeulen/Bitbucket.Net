using System;
using System.Collections.Generic;
using System.Linq;
using Bitbucket.Net.Core.Models.Admin;
using Bitbucket.Net.Core.Models.Logs;
using Bitbucket.Net.Core.Models.Projects;

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

        private static readonly Dictionary<PullRequestFromTypes, string> s_stringByPullRequestFromType = new Dictionary<PullRequestFromTypes, string>
        {
            [PullRequestFromTypes.Comment] = "COMMENT",
            [PullRequestFromTypes.Activity] = "ACTIVITY"
        };

        private static readonly Dictionary<Permissions, string> s_stringByPermissions = new Dictionary<Permissions, string>
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

        private static readonly Dictionary<Roles, string> s_stringByRoles = new Dictionary<Roles, string>
        {
            [Roles.Author] = "AUTHOR",
            [Roles.Reviewer] = "REVIEWER",
            [Roles.Participant] = "PARTICIPANT"
        };

        private static readonly Dictionary<LineTypes, string> s_stringByLineTypes = new Dictionary<LineTypes, string>
        {
            [LineTypes.Added] = "ADDED",
            [LineTypes.Removed] = "REMOVED",
            [LineTypes.Context] = "CONTEXT"
        };

        private static readonly Dictionary<FileTypes, string> s_stringByFileTypes = new Dictionary<FileTypes, string>
        {
            [FileTypes.From] = "FROM",
            [FileTypes.To] = "TO"
        };

        private static readonly Dictionary<ChangeScopes, string> s_stringByChangeScopes = new Dictionary<ChangeScopes, string>
        {
            [ChangeScopes.All] = "ALL",
            [ChangeScopes.Unreviewed] = "UNREVIEWED",
            [ChangeScopes.Range] = "RANGE"
        };

        private static readonly Dictionary<LogLevels, string> s_stringByLogLevels = new Dictionary<LogLevels, string>
        {
            [LogLevels.Trace] = "TRACE",
            [LogLevels.Debug] = "DEBUG",
            [LogLevels.Info] = "INFO",
            [LogLevels.Warn] = "WARN",
            [LogLevels.Error] = "ERROR"
        };

        private static readonly Dictionary<ParticipantStatus, string> s_stringByParticipantStatus = new Dictionary<ParticipantStatus, string>
        {
            [ParticipantStatus.Approved] = "APPROVED",
            [ParticipantStatus.NeedsWork] = "NEEDS_WORK",
            [ParticipantStatus.Unapproved] = "UNAPPROVED"
        };

        public static string BoolToString(bool value) => value
            ? "true"
            : "false";

        public static string BoolToString(bool? value) => value.HasValue
            ? BoolToString(value)
            : null;

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

        public static string PullRequestStateToString(PullRequestStates? state) => state.HasValue 
            ? PullRequestStateToString(state.Value) 
            : null;

        public static PullRequestStates StringToPullRequestState(string s)
        {
            var pair = s_stringByPullRequestState.FirstOrDefault(kvp => kvp.Value.Equals(s, StringComparison.OrdinalIgnoreCase));
            // ReSharper disable once SuspiciousTypeConversion.Global
            if (EqualityComparer<KeyValuePair<PullRequestStates, string>>.Default.Equals(pair))
            {
                throw new ArgumentException($"Unknown pull request state: {s}");
            }

            return pair.Key;
        }

        public static string PullRequestOrderToString(PullRequestOrders order)
        {
            if (!s_stringByPullRequestOrder.TryGetValue(order, out string result))
            {
                throw new ArgumentException($"Unknown pull request order: {order}");
            }

            return result;
        }

        public static string PullRequestOrderToString(PullRequestOrders? order) => order.HasValue
            ? PullRequestOrderToString(order.Value)
            : null;

        private static string PullRequestFromTypeToString(PullRequestFromTypes fromType)
        {
            if (!s_stringByPullRequestFromType.TryGetValue(fromType, out string result))
            {
                throw new ArgumentException($"Unknown pull request from type: {fromType}");
            }

            return result;
        }

        public static string PullRequestFromTypeToString(PullRequestFromTypes? fromType) => fromType.HasValue
            ? PullRequestFromTypeToString(fromType.Value)
            : null;

        public static string PermissionToString(Permissions permission)
        {
            if (!s_stringByPermissions.TryGetValue(permission, out string result))
            {
                throw new ArgumentException($"Unknown permission: {permission}");
            }

            return result;
        }

        public static string PermissionToString(Permissions? permission) => permission.HasValue 
            ? PermissionToString(permission.Value) 
            : null;

        public static Permissions StringToPermission(string s)
        {
            var pair = s_stringByPermissions.FirstOrDefault(kvp => kvp.Value.Equals(s, StringComparison.OrdinalIgnoreCase));
            // ReSharper disable once SuspiciousTypeConversion.Global
            if (EqualityComparer<KeyValuePair<Permissions, string>>.Default.Equals(pair))
            {
                throw new ArgumentException($"Unknown permission: {s}");
            }

            return pair.Key;
        }

        public static string MergeCommitsToString(MergeCommits mergeCommits)
        {
            if (!s_stringByMergeCommits.TryGetValue(mergeCommits, out string result))
            {
                throw new ArgumentException($"Unknown merge commit: {mergeCommits}");
            }

            return result;
        }

        public static string RoleToString(Roles role)
        {
            if (!s_stringByRoles.TryGetValue(role, out string result))
            {
                throw new ArgumentException($"Unknown role: {role}");
            }

            return result;
        }

        public static string RoleToString(Roles? role) => role.HasValue
            ? RoleToString(role.Value)
            : null;

        public static Roles StringToRole(string s)
        {
            var pair = s_stringByRoles.FirstOrDefault(kvp => kvp.Value.Equals(s, StringComparison.OrdinalIgnoreCase));
            // ReSharper disable once SuspiciousTypeConversion.Global
            if (EqualityComparer<KeyValuePair<Roles, string>>.Default.Equals(pair))
            {
                throw new ArgumentException($"Unknown role: {s}");
            }

            return pair.Key;
        }

        public static string LineTypeToString(LineTypes lineType)
        {
            if (!s_stringByLineTypes.TryGetValue(lineType, out string result))
            {
                throw new ArgumentException($"Unknown line type: {lineType}");
            }

            return result;
        }

        public static LineTypes StringToLineType(string s)
        {
            var pair = s_stringByLineTypes.FirstOrDefault(kvp => kvp.Value.Equals(s, StringComparison.OrdinalIgnoreCase));
            // ReSharper disable once SuspiciousTypeConversion.Global
            if (EqualityComparer<KeyValuePair<LineTypes, string>>.Default.Equals(pair))
            {
                throw new ArgumentException($"Unknown line type: {s}");
            }

            return pair.Key;
        }

        public static string FileTypeToString(FileTypes fileType)
        {
            if (!s_stringByFileTypes.TryGetValue(fileType, out string result))
            {
                throw new ArgumentException($"Unknown file type: {fileType}");
            }

            return result;
        }

        public static FileTypes StringToFileType(string s)
        {
            var pair = s_stringByFileTypes.FirstOrDefault(kvp => kvp.Value.Equals(s, StringComparison.OrdinalIgnoreCase));
            // ReSharper disable once SuspiciousTypeConversion.Global
            if (EqualityComparer<KeyValuePair<FileTypes, string>>.Default.Equals(pair))
            {
                throw new ArgumentException($"Unknown file type: {s}");
            }

            return pair.Key;
        }

        public static string ChangeScopeToString(ChangeScopes changeScope)
        {
            if (!s_stringByChangeScopes.TryGetValue(changeScope, out string result))
            {
                throw new ArgumentException($"Unknown change scope: {changeScope}");
            }

            return result;
        }

        public static string LogLevelToString(LogLevels logLevel)
        {
            if (!s_stringByLogLevels.TryGetValue(logLevel, out string result))
            {
                throw new ArgumentException($"Unknown log level: {logLevel}");
            }

            return result;
        }

        public static LogLevels StringToLogLevel(string s)
        {
            var pair = s_stringByLogLevels.FirstOrDefault(kvp => kvp.Value.Equals(s, StringComparison.OrdinalIgnoreCase));
            // ReSharper disable once SuspiciousTypeConversion.Global
            if (EqualityComparer<KeyValuePair<LogLevels, string>>.Default.Equals(pair))
            {
                throw new ArgumentException($"Unknown log level: {s}");
            }

            return pair.Key;
        }

        public static string ParticipantStatusToString(ParticipantStatus participantStatus)
        {
            if (!s_stringByParticipantStatus.TryGetValue(participantStatus, out string result))
            {
                throw new ArgumentException($"Unknown participant status: {participantStatus}");
            }

            return result;
        }
    }
}
