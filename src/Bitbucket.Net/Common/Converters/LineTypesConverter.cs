using Bitbucket.Net.Core.Models.Projects;

namespace Bitbucket.Net.Common.Converters
{
    public class LineTypesConverter : JsonEnumConverter<LineTypes>
    {
        protected override string ConvertToString(LineTypes value)
        {
            return BitbucketHelpers.LineTypeToString(value);
        }
    }
}
