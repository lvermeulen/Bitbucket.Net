using System;
using Bitbucket.Net.Common.Converters;
using Bitbucket.Net.Models.Projects;
using Bitbucket.Net.Models.Users;
using Newtonsoft.Json;

namespace Bitbucket.Net.Models.Tasks
{
    public abstract class TaskRef
    {
        public Properties Properties { get; set; }
        public int Id { get; set; }
        public string Text { get; set; }
        public User Author { get; set; }
        [JsonConverter(typeof(UnixDateTimeOffsetConverter))]
        public DateTimeOffset? CreatedDate { get; set; }
        public Permittedoperations PermittedOperations { get; set; }
    }
}