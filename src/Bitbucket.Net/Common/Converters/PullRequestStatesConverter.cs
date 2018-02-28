using Bitbucket.Net.Core.Models.Projects;

namespace Bitbucket.Net.Common.Converters
{
    public class PullRequestStatesConverter : JsonEnumConverter<PullRequestStates>
    {
        protected override string ConvertToString(PullRequestStates value)
        {
            return BitbucketHelpers.PullRequestStateToString(value);
        }
    }
}
