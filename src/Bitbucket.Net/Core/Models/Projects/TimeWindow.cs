using System;
using Bitbucket.Net.Common.Converters;
using Newtonsoft.Json;

namespace Bitbucket.Net.Core.Models.Projects
{
    public class TimeWindow
    {
        [JsonConverter(typeof(UnixDateTimeOffsetConverter))]
        public DateTimeOffset Start { get; set; }
        public long Duration { get; set; }
    }
}