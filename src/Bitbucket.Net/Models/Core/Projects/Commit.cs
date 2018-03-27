using System;
using System.Collections.Generic;
using Bitbucket.Net.Common.Converters;
using Newtonsoft.Json;

namespace Bitbucket.Net.Models.Core.Projects
{
    public class Commit
    {
        public string Id { get; set; }
        public string DisplayId { get; set; }
        public Author Author { get; set; }
        [JsonConverter(typeof(UnixDateTimeOffsetConverter))]
        public DateTimeOffset AuthorTimestamp { get; set; }
        public Author Committer { get; set; }
        [JsonConverter(typeof(UnixDateTimeOffsetConverter))]
        public DateTimeOffset CommitterTimestamp { get; set; }
        public string Message { get; set; }
        public List<CommitParent> Parents { get; set; }
        public int AuthorCount { get; set; }
        public int TotalCount { get; set; }
    }
}
