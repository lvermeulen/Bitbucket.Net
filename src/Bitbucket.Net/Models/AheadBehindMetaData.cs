using Newtonsoft.Json;

namespace Bitbucket.Net.Models
{
    public class AheadBehindMetaData
    {
        [JsonProperty("ahead")]
        public int Ahead { get; set; }
        [JsonProperty("behind")]
        public int Behind { get; set; }
    }
}
