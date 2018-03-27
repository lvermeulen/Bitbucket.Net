using Bitbucket.Net.Models.Core.Admin;

namespace Bitbucket.Net.Common.Converters
{
    public class PermissionsConverter : JsonEnumConverter<Permissions>
    {
        protected override string ConvertToString(Permissions value)
        {
            return BitbucketHelpers.PermissionToString(value);
        }

        protected override Permissions ConvertFromString(string s)
        {
            return BitbucketHelpers.StringToPermission(s);
        }
    }
}
