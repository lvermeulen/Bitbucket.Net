using Bitbucket.Net.Models.Projects;

namespace Bitbucket.Net.Common.Converters
{
    public class RolesConverter : JsonEnumConverter<Roles>
    {
        protected override string ConvertToString(Roles value)
        {
            return BitbucketHelpers.RoleToString(value);
        }

        protected override Roles ConvertFromString(string s)
        {
            return BitbucketHelpers.StringToRole(s);
        }
    }
}
