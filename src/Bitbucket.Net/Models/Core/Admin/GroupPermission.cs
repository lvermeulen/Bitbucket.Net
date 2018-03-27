using Bitbucket.Net.Common.Converters;
using Bitbucket.Net.Models.Core.Users;
using Newtonsoft.Json;

namespace Bitbucket.Net.Models.Core.Admin
{
    public class GroupPermission
    {
        public Named Group { get; set; }
        [JsonConverter(typeof(PermissionsConverter))]
        public Permissions Permission { get; set; }

        public override string ToString() => $"{Permission} - {Group}";
    }
}
