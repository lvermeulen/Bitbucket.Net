using System.Collections.Generic;
using Bitbucket.Net.Common.Converters;
using Newtonsoft.Json;

namespace Bitbucket.Net.Models.Core.Projects
{
    public class HookDetails
    {
        public string Key { get; set; }
        public string Name { get; set; }
        [JsonConverter(typeof(HookTypesConverter))]
        public HookTypes Type { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public object ConfigFormKey { get; set; }
        public List<ScopeTypes> ScopeTypes { get; set; }
    }
}