using System;
using System.Collections.Generic;
using Bitbucket.Net.Common.Converters;
using Bitbucket.Net.Models.Core.Tasks;
using Bitbucket.Net.Models.Core.Users;
using Newtonsoft.Json;

namespace Bitbucket.Net.Models.Core.Projects
{
    public class Comment : PullRequestInfo
    {
        public int Id { get; set; }
        public int Version { get; set; }
        public string Text { get; set; }
        [JsonConverter(typeof(UnixDateTimeOffsetConverter))]
        public DateTimeOffset? CreatedDate { get; set; }
        [JsonConverter(typeof(UnixDateTimeOffsetConverter))]
        public DateTimeOffset? UpdatedDate { get; set; }
        public User Author { get; set; }
        public List<Comment> Comments { get; set; }
        public List<BitbucketTask> Tasks { get; set; }
        public List<Participant> Participants { get; set; }
        public Links Links { get; set; }
    }
}
