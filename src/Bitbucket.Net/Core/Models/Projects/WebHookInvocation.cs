using System;
using Bitbucket.Net.Common.Converters;
using Newtonsoft.Json;

namespace Bitbucket.Net.Core.Models.Projects
{
    public class WebHookInvocation
    {
        public int Id { get; set; }
        public string Event { get; set; }
        public int Duration { get; set; }
        [JsonConverter(typeof(UnixDateTimeOffsetConverter))]
        public DateTimeOffset Start { get; set; }
        [JsonConverter(typeof(UnixDateTimeOffsetConverter))]
        public DateTimeOffset Finish { get; set; }
        public WebHookRequest Request { get; set; }
        public WebHookResult Result { get; set; }
    }
}