using Bitbucket.Net.Models.RefRestrictions;

namespace Bitbucket.Net.Common.Converters
{
    public class RefRestrictionTypesConverter : JsonEnumConverter<RefRestrictionTypes>
    {
        protected override string ConvertToString(RefRestrictionTypes value)
        {
            return BitbucketHelpers.RefRestrictionTypeToString(value);
        }

        protected override RefRestrictionTypes ConvertFromString(string s)
        {
            return BitbucketHelpers.StringToRefRestrictionType(s);
        }
    }
}
