using System;
using System.Collections.Generic;
using System.Linq;
using Bitbucket.Net.Models.Core.Admin;
using Bitbucket.Net.Models.Core.Logs;
using Bitbucket.Net.Models.Core.Projects;
using Bitbucket.Net.Models.Git;
using Bitbucket.Net.Models.RefRestrictions;
using Bitbucket.Net.Models.RefSync;

namespace Bitbucket.Net.Common
{
    public static class BitbucketHelpers
    {
        #region Bool

        public static string BoolToString(bool value) => value
            ? "true"
            : "false";

        public static string BoolToString(bool? value) => value.HasValue
            ? BoolToString(value)
            : null;

        public static bool StringToBool(string value) => value.Equals("true", StringComparison.OrdinalIgnoreCase);

        #endregion

        #region BranchOrderBy

        private static readonly Dictionary<BranchOrderBy, string> s_stringByBranchOrderBy = new Dictionary<BranchOrderBy, string>
        {
            [BranchOrderBy.Alphabetical] = "ALPHABETICAL",
            [BranchOrderBy.Modification] = "MODIFICATION"
        };

        public static string BranchOrderByToString(BranchOrderBy orderBy)
        {
            if (!s_stringByBranchOrderBy.TryGetValue(orderBy, out string result))
            {
                throw new ArgumentException($"Unknown branch order by: {orderBy}");
            }

            return result;
        }

        #endregion

        #region PullRequestDirections

        private static readonly Dictionary<PullRequestDirections, string> s_stringByPullRequestDirection = new Dictionary<PullRequestDirections, string>
        {
            [PullRequestDirections.Incoming] = "INCOMING",
            [PullRequestDirections.Outgoing] = "OUTGOING"
        };

        public static string PullRequestDirectionToString(PullRequestDirections direction)
        {
            if (!s_stringByPullRequestDirection.TryGetValue(direction, out string result))
            {
                throw new ArgumentException($"Unknown pull request direction: {direction}");
            }

            return result;
        }

        #endregion

        #region PullRequestStates

        private static readonly Dictionary<PullRequestStates, string> s_stringByPullRequestState = new Dictionary<PullRequestStates, string>
        {
            [PullRequestStates.Open] = "OPEN",
            [PullRequestStates.Declined] = "DECLINED",
            [PullRequestStates.Merged] = "MERGED",
            [PullRequestStates.All] = "ALL"
        };

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

        #endregion

        #region PullRequestOrders

        private static readonly Dictionary<PullRequestOrders, string> s_stringByPullRequestOrder = new Dictionary<PullRequestOrders, string>
        {
            [PullRequestOrders.Newest] = "NEWEST",
            [PullRequestOrders.Oldest] = "OLDEST"
        };

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

        #endregion

        #region PullRequestFromTypes

        private static readonly Dictionary<PullRequestFromTypes, string> s_stringByPullRequestFromType = new Dictionary<PullRequestFromTypes, string>
        {
            [PullRequestFromTypes.Comment] = "COMMENT",
            [PullRequestFromTypes.Activity] = "ACTIVITY"
        };

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

        #endregion

        #region Permissions

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

        #endregion

        #region MergeCommits

        private static readonly Dictionary<MergeCommits, string> s_stringByMergeCommits = new Dictionary<MergeCommits, string>
        {
            [MergeCommits.Exclude] = "exclude",
            [MergeCommits.Include] = "include",
            [MergeCommits.Only] = "only" 
        };

        public static string MergeCommitsToString(MergeCommits mergeCommits)
        {
            if (!s_stringByMergeCommits.TryGetValue(mergeCommits, out string result))
            {
                throw new ArgumentException($"Unknown merge commit: {mergeCommits}");
            }

            return result;
        }

        #endregion

        #region Roles

        private static readonly Dictionary<Roles, string> s_stringByRoles = new Dictionary<Roles, string>
        {
            [Roles.Author] = "AUTHOR",
            [Roles.Reviewer] = "REVIEWER",
            [Roles.Participant] = "PARTICIPANT"
        };

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

        #endregion

        #region LineTypes

        private static readonly Dictionary<LineTypes, string> s_stringByLineTypes = new Dictionary<LineTypes, string>
        {
            [LineTypes.Added] = "ADDED",
            [LineTypes.Removed] = "REMOVED",
            [LineTypes.Context] = "CONTEXT"
        };

        public static string LineTypeToString(LineTypes lineType)
        {
            if (!s_stringByLineTypes.TryGetValue(lineType, out string result))
            {
                throw new ArgumentException($"Unknown line type: {lineType}");
            }

            return result;
        }

        public static string LineTypeToString(LineTypes? lineType)
        {
            return lineType.HasValue
                ? LineTypeToString(lineType.Value)
                : null;
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

        #endregion

        #region FileTypes

        private static readonly Dictionary<FileTypes, string> s_stringByFileTypes = new Dictionary<FileTypes, string>
        {
            [FileTypes.From] = "FROM",
            [FileTypes.To] = "TO"
        };

        public static string FileTypeToString(FileTypes fileType)
        {
            if (!s_stringByFileTypes.TryGetValue(fileType, out string result))
            {
                throw new ArgumentException($"Unknown file type: {fileType}");
            }

            return result;
        }

        public static string FileTypeToString(FileTypes? fileType)
        {
            return fileType.HasValue
                ? FileTypeToString(fileType.Value)
                : null;
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

        #endregion

        #region ChangeScopes

        private static readonly Dictionary<ChangeScopes, string> s_stringByChangeScopes = new Dictionary<ChangeScopes, string>
        {
            [ChangeScopes.All] = "ALL",
            [ChangeScopes.Unreviewed] = "UNREVIEWED",
            [ChangeScopes.Range] = "RANGE"
        };

        public static string ChangeScopeToString(ChangeScopes changeScope)
        {
            if (!s_stringByChangeScopes.TryGetValue(changeScope, out string result))
            {
                throw new ArgumentException($"Unknown change scope: {changeScope}");
            }

            return result;
        }

        #endregion

        #region LogLevels

        private static readonly Dictionary<LogLevels, string> s_stringByLogLevels = new Dictionary<LogLevels, string>
        {
            [LogLevels.Trace] = "TRACE",
            [LogLevels.Debug] = "DEBUG",
            [LogLevels.Info] = "INFO",
            [LogLevels.Warn] = "WARN",
            [LogLevels.Error] = "ERROR"
        };

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

        #endregion

        #region ParticipantStatus

        private static readonly Dictionary<ParticipantStatus, string> s_stringByParticipantStatus = new Dictionary<ParticipantStatus, string>
        {
            [ParticipantStatus.Approved] = "APPROVED",
            [ParticipantStatus.NeedsWork] = "NEEDS_WORK",
            [ParticipantStatus.Unapproved] = "UNAPPROVED"
        };

        public static string ParticipantStatusToString(ParticipantStatus participantStatus)
        {
            if (!s_stringByParticipantStatus.TryGetValue(participantStatus, out string result))
            {
                throw new ArgumentException($"Unknown participant status: {participantStatus}");
            }

            return result;
        }

        public static ParticipantStatus StringToParticipantStatus(string s)
        {
            var pair = s_stringByParticipantStatus.FirstOrDefault(kvp => kvp.Value.Equals(s, StringComparison.OrdinalIgnoreCase));
            // ReSharper disable once SuspiciousTypeConversion.Global
            if (EqualityComparer<KeyValuePair<ParticipantStatus, string>>.Default.Equals(pair))
            {
                throw new ArgumentException($"Unknown participant status: {s}");
            }

            return pair.Key;
        }

        #endregion

        #region HookTypes

        private static readonly Dictionary<HookTypes, string> s_stringByHookTypes = new Dictionary<HookTypes, string>
        {
            [HookTypes.PreReceive] = "PRE_RECEIVE",
            [HookTypes.PostReceive] = "POST_RECEIVE",
            [HookTypes.PrePullRequestMerge] = "PRE_PULL_REQUEST_MERGE"
        };

        public static string HookTypeToString(HookTypes hookType)
        {
            if (!s_stringByHookTypes.TryGetValue(hookType, out string result))
            {
                throw new ArgumentException($"Unknown hook type: {hookType}");
            }

            return result;
        }

        public static HookTypes StringToHookType(string s)
        {
            var pair = s_stringByHookTypes.FirstOrDefault(kvp => kvp.Value.Equals(s, StringComparison.OrdinalIgnoreCase));
            // ReSharper disable once SuspiciousTypeConversion.Global
            if (EqualityComparer<KeyValuePair<HookTypes, string>>.Default.Equals(pair))
            {
                throw new ArgumentException($"Unknown hook type: {s}");
            }

            return pair.Key;
        }

        #endregion

        #region ScopeTypes

        private static readonly Dictionary<ScopeTypes, string> s_stringByScopeTypes = new Dictionary<ScopeTypes, string>
        {
            [ScopeTypes.Project] = "PROJECT",
            [ScopeTypes.Repository] = "REPOSITORY"
        };

        public static string ScopeTypeToString(ScopeTypes scopeType)
        {
            if (!s_stringByScopeTypes.TryGetValue(scopeType, out string result))
            {
                throw new ArgumentException($"Unknown scope type: {scopeType}");
            }

            return result;
        }

        public static ScopeTypes StringToScopeType(string s)
        {
            var pair = s_stringByScopeTypes.FirstOrDefault(kvp => kvp.Value.Equals(s, StringComparison.OrdinalIgnoreCase));
            // ReSharper disable once SuspiciousTypeConversion.Global
            if (EqualityComparer<KeyValuePair<ScopeTypes, string>>.Default.Equals(pair))
            {
                throw new ArgumentException($"Unknown scope type: {s}");
            }

            return pair.Key;
        }

        #endregion

        #region ArchiveFormats

        private static readonly Dictionary<ArchiveFormats, string> s_stringByArchiveFormats = new Dictionary<ArchiveFormats, string>
        {
            [ArchiveFormats.Zip] = "zip",
            [ArchiveFormats.Tar] = "tar",
            [ArchiveFormats.TarGz] = "tar.gz",
            [ArchiveFormats.Tgz] = "tgz"
        };

        public static string ArchiveFormatToString(ArchiveFormats archiveFormat)
        {
            if (!s_stringByArchiveFormats.TryGetValue(archiveFormat, out string result))
            {
                throw new ArgumentException($"Unknown archive format: {archiveFormat}");
            }

            return result;
        }

        #endregion

        #region WebHookOutcomes

        private static readonly Dictionary<WebHookOutcomes, string> s_stringByWebHookOutcomes = new Dictionary<WebHookOutcomes, string>
        {
            [WebHookOutcomes.Success] = "SUCCESS",
            [WebHookOutcomes.Failure] = "FAILURE",
            [WebHookOutcomes.Error] = "ERROR"
        };

        public static string WebHookOutcomeToString(WebHookOutcomes webHookOutcome)
        {
            if (!s_stringByWebHookOutcomes.TryGetValue(webHookOutcome, out string result))
            {
                throw new ArgumentException($"Unknown web hook outcome: {webHookOutcome}");
            }

            return result;
        }

        public static string WebHookOutcomeToString(WebHookOutcomes? webHookOutcome) => webHookOutcome.HasValue
            ? WebHookOutcomeToString(webHookOutcome.Value)
            : null;

        public static WebHookOutcomes StringToWebHookOutcome(string s)
        {
            var pair = s_stringByWebHookOutcomes.FirstOrDefault(kvp => kvp.Value.Equals(s, StringComparison.OrdinalIgnoreCase));
            // ReSharper disable once SuspiciousTypeConversion.Global
            if (EqualityComparer<KeyValuePair<WebHookOutcomes, string>>.Default.Equals(pair))
            {
                throw new ArgumentException($"Unknown web hook outcome: {s}");
            }

            return pair.Key;
        }

        #endregion

        #region AnchorStates

        private static readonly Dictionary<AnchorStates, string> s_stringByAnchorStates = new Dictionary<AnchorStates, string>
        {
            [AnchorStates.Active] = "ACTIVE",
            [AnchorStates.Orphaned] = "ORPHANED",
            [AnchorStates.All] = "ALL"
        };

        public static string AnchorStateToString(AnchorStates anchorState)
        {
            if (!s_stringByAnchorStates.TryGetValue(anchorState, out string result))
            {
                throw new ArgumentException($"Unknown anchor state: {anchorState}");
            }

            return result;
        }

        #endregion

        #region DiffTypes

        private static readonly Dictionary<DiffTypes, string> s_stringByDiffTypes = new Dictionary<DiffTypes, string>
        {
            [DiffTypes.Effective] = "EFFECTIVE",
            [DiffTypes.Range] = "RANGE",
            [DiffTypes.Commit] = "COMMIT"
        };

        public static string DiffTypeToString(DiffTypes diffType)
        {
            if (!s_stringByDiffTypes.TryGetValue(diffType, out string result))
            {
                throw new ArgumentException($"Unknown diff type: {diffType}");
            }

            return result;
        }

        public static string DiffTypeToString(DiffTypes? diffType)
        {
            return diffType.HasValue
                ? DiffTypeToString(diffType.Value)
                : null;
        }

        #endregion

        #region TagTypes

        private static readonly Dictionary<TagTypes, string> s_stringByTagTypes = new Dictionary<TagTypes, string>
        {
            [TagTypes.LightWeight] = "LIGHTWEIGHT",
            [TagTypes.Annotated] = "ANNOTATED"
        };

        public static string TagTypeToString(TagTypes tagType)
        {
            if (!s_stringByTagTypes.TryGetValue(tagType, out string result))
            {
                throw new ArgumentException($"Unknown tag type: {tagType}");
            }

            return result;
        }

        #endregion

        #region RefRestrictionTypes

        private static readonly Dictionary<RefRestrictionTypes, string> s_stringByRefRestrictionTypes = new Dictionary<RefRestrictionTypes, string>
        {
            [RefRestrictionTypes.AllChanges] = "read-only",
            [RefRestrictionTypes.RewritingHistory] = "fast-forward-only",
            [RefRestrictionTypes.Deletion] = "no-deletes",
            [RefRestrictionTypes.ChangesWithoutPullRequest] = "pull-request-only"
        };

        public static string RefRestrictionTypeToString(RefRestrictionTypes refRestrictionType)
        {
            if (!s_stringByRefRestrictionTypes.TryGetValue(refRestrictionType, out string result))
            {
                throw new ArgumentException($"Unknown ref restriction type: {refRestrictionType}");
            }

            return result;
        }

        public static string RefRestrictionTypeToString(RefRestrictionTypes? refRestrictionType)
        {
            return refRestrictionType.HasValue
                ? RefRestrictionTypeToString(refRestrictionType.Value)
                : null;
        }

        public static RefRestrictionTypes StringToRefRestrictionType(string s)
        {
            var pair = s_stringByRefRestrictionTypes.FirstOrDefault(kvp => kvp.Value.Equals(s, StringComparison.OrdinalIgnoreCase));
            // ReSharper disable once SuspiciousTypeConversion.Global
            if (EqualityComparer<KeyValuePair<RefRestrictionTypes, string>>.Default.Equals(pair))
            {
                throw new ArgumentException($"Unknown ref restriction type: {s}");
            }

            return pair.Key;
        }

        #endregion

        #region RefMatcherTypes

        private static readonly Dictionary<RefMatcherTypes, string> s_stringByRefMatcherTypes = new Dictionary<RefMatcherTypes, string>
        {
            [RefMatcherTypes.Branch] = "BRANCH",
            [RefMatcherTypes.Pattern] = "PATTERN",
            [RefMatcherTypes.ModelCategory] = "MODEL_CATEGORY",
            [RefMatcherTypes.ModelBranch] = "MODEL_BRANCH"
        };

        private static string RefMatcherTypeToString(RefMatcherTypes refMatcherType)
        {
            if (!s_stringByRefMatcherTypes.TryGetValue(refMatcherType, out string result))
            {
                throw new ArgumentException($"Unknown ref matcher type: {refMatcherType}");
            }

            return result;
        }

        public static string RefMatcherTypeToString(RefMatcherTypes? refMatcherType)
        {
            return refMatcherType.HasValue
                ? RefMatcherTypeToString(refMatcherType.Value)
                : null;
        }

        #endregion

        #region SynchronizeActions

        private static readonly Dictionary<SynchronizeActions, string> s_stringBySynchronizeActions = new Dictionary<SynchronizeActions, string>
        {
            [SynchronizeActions.Merge] = "MERGE",
            [SynchronizeActions.Discard] = "DISCARD"
        };

        public static string SynchronizeActionToString(SynchronizeActions synchronizeAction)
        {
            if (!s_stringBySynchronizeActions.TryGetValue(synchronizeAction, out string result))
            {
                throw new ArgumentException($"Unknown synchronize action: {synchronizeAction}");
            }

            return result;
        }

        public static SynchronizeActions StringToSynchronizeAction(string s)
        {
            var pair = s_stringBySynchronizeActions.FirstOrDefault(kvp => kvp.Value.Equals(s, StringComparison.OrdinalIgnoreCase));
            // ReSharper disable once SuspiciousTypeConversion.Global
            if (EqualityComparer<KeyValuePair<SynchronizeActions, string>>.Default.Equals(pair))
            {
                throw new ArgumentException($"Unknown synchronize action: {s}");
            }

            return pair.Key;
        }

        #endregion
    }
}
