using Bitbucket.Net.Common.Converters;
using Newtonsoft.Json;

namespace Bitbucket.Net.Models.RefSync
{
    public class Synchronize
    {
        public string RefId { get; set; }
        [JsonConverter(typeof(SynchronizeActionsConverter))]
        public SynchronizeActions Action { get; set; }
        public SynchronizeContext Context { get; set; }
    }
}
