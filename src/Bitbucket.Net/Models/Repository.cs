using Newtonsoft.Json;

namespace Bitbucket.Net.Models
{
    public class Repository : RepositoryRef
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("ccmId")]
        public string CcmId { get; set; }
        [JsonProperty("state")]
        public string State { get; set; }
        [JsonProperty("statusMessage")]
        public string StatusMessage { get; set; }
        [JsonProperty("forkable")]
        public bool Forkable { get; set; }
        [JsonProperty("public")]
        public bool Public { get; set; }
        [JsonProperty("links")]
        public CloneLinks Links { get; set; }

        public override string ToString() => Name;
    }
}
