using Newtonsoft.Json;

namespace Bitbucket.Net.Models
{
    public class ProjectRef
    {
        [JsonProperty("key")]
        public string Key { get; set; }
    }
}
