using Bitbucket.Net.Common.Converters;
using Newtonsoft.Json;

namespace Bitbucket.Net.Models.Core.Projects
{
    public class WebHookResult
    {
        public string Description { get; set; }
        [JsonConverter(typeof(WebHookOutcomesConverter))]
        public WebHookOutcomes Outcome { get; set; }
    }
}
