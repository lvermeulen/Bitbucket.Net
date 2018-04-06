using Bitbucket.Net.Models.RefSync;

namespace Bitbucket.Net.Common.Converters
{
    public class SynchronizeActionsConverter : JsonEnumConverter<SynchronizeActions>
    {
        protected override string ConvertToString(SynchronizeActions value)
        {
            return BitbucketHelpers.SynchronizeActionToString(value);
        }

        protected override SynchronizeActions ConvertFromString(string s)
        {
            return BitbucketHelpers.StringToSynchronizeAction(s);
        }
    }
}
