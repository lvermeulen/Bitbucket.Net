using Bitbucket.Net.Core.Models.Admin;

namespace Bitbucket.Net.Common.Converters
{
    public class PermissionsConverter : JsonEnumConverter<Permissions>
    {
        protected override string ConvertToString(Permissions value)
        {
            return BitbucketHelpers.PermissionToString(value);
        }
    }
}
