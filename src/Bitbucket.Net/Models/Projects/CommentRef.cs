using System;
using System.Collections.Generic;
using Bitbucket.Net.Common;
using Newtonsoft.Json;

namespace Bitbucket.Net.Models.Projects
{
    public class CommentRef
    {
        public Properties Properties { get; set; }
        public int Id { get; set; }
        public int Version { get; set; }
        public string Text { get; set; }
        public User Author { get; set; }
        [JsonConverter(typeof(UnixDateTimeOffsetConverter))]
        public DateTimeOffset? CreatedDate { get; set; }
        [JsonConverter(typeof(UnixDateTimeOffsetConverter))]
        public DateTimeOffset? UpdatedDate { get; set; }
        public List<CommentRef> Comments { get; set; }
        public List<object> Tasks { get; set; }
        public Permittedoperations PermittedOperations { get; set; }
    }
}