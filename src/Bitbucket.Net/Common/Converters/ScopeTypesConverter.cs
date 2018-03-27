using Bitbucket.Net.Models.Core.Projects;

namespace Bitbucket.Net.Common.Converters
{
    public class ScopeTypesConverter : JsonEnumConverter<ScopeTypes>
    {
        protected override string ConvertToString(ScopeTypes value)
        {
            return BitbucketHelpers.ScopeTypeToString(value);
        }

        protected override ScopeTypes ConvertFromString(string s)
        {
            return BitbucketHelpers.StringToScopeType(s);
        }
    }
}
