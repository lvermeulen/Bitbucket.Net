using Bitbucket.Net.Common.Converters;
using Bitbucket.Net.Models.Core.Users;
using Newtonsoft.Json;

namespace Bitbucket.Net.Models.Core.Admin
{
    public class UserPermission
    {
        public User User { get; set; }
        [JsonConverter(typeof(PermissionsConverter))]
        public Permissions Permission { get; set; }

        public override string ToString() => $"{Permission} - {User?.DisplayName}";
    }
}
