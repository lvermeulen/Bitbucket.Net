using Bitbucket.Net.Common.Converters;
using Bitbucket.Net.Models.Users;
using Newtonsoft.Json;

namespace Bitbucket.Net.Models.Projects
{
    public class Participant
    {
        public User User { get; set; }
        [JsonConverter(typeof(RolesConverter))]
        public Roles Role { get; set; }
        public bool Approved { get; set; }
        public string Status { get; set; }

        public override string ToString() => User.DisplayName;
    }
}
