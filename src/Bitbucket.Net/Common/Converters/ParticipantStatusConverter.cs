using Bitbucket.Net.Models.Core.Projects;

namespace Bitbucket.Net.Common.Converters
{
    public class ParticipantStatusConverter : JsonEnumConverter<ParticipantStatus>
    {
        protected override string ConvertToString(ParticipantStatus value)
        {
            return BitbucketHelpers.ParticipantStatusToString(value);
        }

        protected override ParticipantStatus ConvertFromString(string s)
        {
            return BitbucketHelpers.StringToParticipantStatus(s);
        }
    }
}
