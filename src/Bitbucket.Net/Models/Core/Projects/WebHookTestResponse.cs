using System.Collections.Generic;

namespace Bitbucket.Net.Models.Core.Projects
{
    public class WebHookTestResponse
    {
        public int Status { get; set; }
        public List<string> Headers { get; set; }
        public string Body { get; set; }
    }
}