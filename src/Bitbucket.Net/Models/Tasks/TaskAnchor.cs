using System;
using System.Collections.Generic;
using Bitbucket.Net.Common;
using Bitbucket.Net.Models.Projects;
using Newtonsoft.Json;

namespace Bitbucket.Net.Models.Tasks
{
    public class TaskAnchor : TaskRef
    {
        public int Version { get; set; }
        [JsonConverter(typeof(UnixDateTimeOffsetConverter))]
        public DateTimeOffset? UpdatedDate { get; set; }
        public List<CommentRef> Comments { get; set; }
        public List<BitbucketTask> Tasks { get; set; }
    }
}