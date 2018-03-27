using Bitbucket.Net.Common.Converters;
using Newtonsoft.Json;

namespace Bitbucket.Net.Models.Core.Projects
{
    public class HookScope
    {
        public int ResourceId { get; set; }
        [JsonConverter(typeof(ScopeTypesConverter))]
        public ScopeTypes Type { get; set; }
    }
}