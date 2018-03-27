using Bitbucket.Net.Models.Core.Projects;

namespace Bitbucket.Net.Common.Converters
{
    public class PullRequestStatesConverter : JsonEnumConverter<PullRequestStates>
    {
        protected override string ConvertToString(PullRequestStates value)
        {
            return BitbucketHelpers.PullRequestStateToString(value);
        }

        protected override PullRequestStates ConvertFromString(string s)
        {
            return BitbucketHelpers.StringToPullRequestState(s);
        }
    }
}
