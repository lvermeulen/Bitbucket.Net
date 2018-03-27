using Bitbucket.Net.Common.Converters;
using Bitbucket.Net.Models.Core.Users;
using Newtonsoft.Json;

namespace Bitbucket.Net.Models.Core.Projects
{
    public class Participant
    {
        public User User { get; set; }
        [JsonConverter(typeof(RolesConverter))]
        public Roles Role { get; set; }
        public bool Approved { get; set; }
        [JsonConverter(typeof(ParticipantStatusConverter))]
        public ParticipantStatus Status { get; set; }

        public override string ToString() => User.DisplayName;
    }
}
