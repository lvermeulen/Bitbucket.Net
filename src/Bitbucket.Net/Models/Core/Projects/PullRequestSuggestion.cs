using System;
using Bitbucket.Net.Common.Converters;
using Newtonsoft.Json;

namespace Bitbucket.Net.Models.Core.Projects
{
    public class PullRequestSuggestion
    {
        [JsonConverter(typeof(UnixDateTimeOffsetConverter))]
        public DateTimeOffset ChangeTime { get; set; }
        public RefChange RefChange { get; set; }
        public Repository Repository { get; set; }
        public Ref FromRef { get; set; }
        public Ref ToRef { get; set; }
    }
}
