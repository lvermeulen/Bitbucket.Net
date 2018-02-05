using Newtonsoft.Json;

namespace Bitbucket.Net.Models
{
    public class RepositoryRef
    {
        [JsonProperty("slug")]
        public string Slug { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("project")]
        public ProjectRef Project { get; set; }
    }
}
