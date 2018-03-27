using Bitbucket.Net.Models.Core.Projects;

namespace Bitbucket.Net.Common.Converters
{
    public class HookTypesConverter : JsonEnumConverter<HookTypes>
    {
        protected override string ConvertToString(HookTypes value)
        {
            return BitbucketHelpers.HookTypeToString(value);
        }

        protected override HookTypes ConvertFromString(string s)
        {
            return BitbucketHelpers.StringToHookType(s);
        }
    }
}
