using System.Collections.Generic;
using Bitbucket.Net.Common.Converters;
using Bitbucket.Net.Models.Core.Admin;
using Newtonsoft.Json;

namespace Bitbucket.Net.Models.PersonalAccessTokens
{
    public class AccessTokenCreate
    {
        public string Name { get; set; }
        [JsonConverter(typeof(PermissionsConverter))]
        public List<Permissions> Permissions { get; set; }
    }
}