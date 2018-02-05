using Newtonsoft.Json;

namespace Bitbucket.Net.Models
{
    public class FromToRef
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("repository")]
        public RepositoryRef Repository { get; set; }
    }
}