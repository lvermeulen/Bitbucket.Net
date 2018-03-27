using Bitbucket.Net.Models.Core.Projects;

namespace Bitbucket.Net.Common.Converters
{
    public class LineTypesConverter : JsonEnumConverter<LineTypes>
    {
        protected override string ConvertToString(LineTypes value)
        {
            return BitbucketHelpers.LineTypeToString(value);
        }

        protected override LineTypes ConvertFromString(string s)
        {
            return BitbucketHelpers.StringToLineType(s);
        }
    }
}
