using Bitbucket.Net.Common;
using Newtonsoft.Json;

namespace Bitbucket.Net.Models.Projects
{
    public class GroupPermission
    {
        public Group Group { get; set; }
        [JsonConverter(typeof(PermissionsConverter))]
        public Permissions Permission { get; set; }

        public override string ToString() => $"{Permission} - {Group}";
    }
}
