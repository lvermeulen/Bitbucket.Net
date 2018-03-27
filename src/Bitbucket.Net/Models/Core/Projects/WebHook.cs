using System;
using System.Collections.Generic;
using Bitbucket.Net.Common.Converters;
using Newtonsoft.Json;

namespace Bitbucket.Net.Models.Core.Projects
{
    public class WebHook
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonConverter(typeof(UnixDateTimeOffsetConverter))]
        public DateTimeOffset CreatedDate { get; set; }
        [JsonConverter(typeof(UnixDateTimeOffsetConverter))]
        public DateTimeOffset UpdatedDate { get; set; }
        public List<string> Events { get; set; }
        public Dictionary<string, object> Configuration { get; set; }
        public string Url { get; set; }
        public bool Active { get; set; }
    }
}
