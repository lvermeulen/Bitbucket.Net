using System.Collections.Generic;

namespace Bitbucket.Net.Core.Models.Projects
{
    public class WebHookTestRequest : WebHookRequest
    {
        public string Body { get; set; }
        public List<string> Headers { get; set; }
    }
}