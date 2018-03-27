using System;
using Bitbucket.Net.Common.Converters;
using Newtonsoft.Json;

namespace Bitbucket.Net.Models.Core.Projects
{
    public class TimeWindow
    {
        [JsonConverter(typeof(UnixDateTimeOffsetConverter))]
        public DateTimeOffset Start { get; set; }
        public long Duration { get; set; }
    }
}