using Bitbucket.Net.Common.Converters;
using Bitbucket.Net.Models.Core.Users;
using Newtonsoft.Json;
using System;

namespace Bitbucket.Net.Models.Core.Projects
{
    public class PullRequestActivity
    {
        public int Id { get; set; }
        [JsonConverter(typeof(UnixDateTimeOffsetConverter))]
        public DateTimeOffset? CreatedDate { get; set; }
        public User User { get; set; }
        public string Action { get; set; }
        public string CommentAction { get; set; }
        public Comment Comment { get; set; }
        public CommentAnchor CommentAnchor { get; set; }
    }
}
