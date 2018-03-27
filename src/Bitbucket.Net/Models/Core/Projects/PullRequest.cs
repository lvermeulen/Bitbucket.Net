using System;
using System.Collections.Generic;
using Bitbucket.Net.Common.Converters;
using Newtonsoft.Json;

namespace Bitbucket.Net.Models.Core.Projects
{
    public class PullRequest : PullRequestInfo
    {
        public int Id { get; set; }
        public int Version { get; set; }
        [JsonConverter(typeof(UnixDateTimeOffsetConverter))]
        public DateTimeOffset? CreatedDate { get; set; }
        [JsonConverter(typeof(UnixDateTimeOffsetConverter))]
        public DateTimeOffset? UpdatedDate { get; set; }
        public Participant Author { get; set; }
        public List<Participant> Participants { get; set; }
        public Links Links { get; set; }

        public override string ToString() => $"{Author.User.DisplayName}: {Title ?? "(untitled)"}";
    }
}
