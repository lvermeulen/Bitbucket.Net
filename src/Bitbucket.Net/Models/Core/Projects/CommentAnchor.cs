using Bitbucket.Net.Common.Converters;
using Newtonsoft.Json;

namespace Bitbucket.Net.Models.Core.Projects
{
    public class CommentAnchor
    {
        public int? Line { get; set; }
        [JsonConverter(typeof(LineTypesConverter))]
        public LineTypes LineType { get; set; }
        [JsonConverter(typeof(FileTypesConverter))]
        public FileTypes FileType { get; set; }
        public string Path { get; set; }
        public string SrcPath { get; set; }
    }
}