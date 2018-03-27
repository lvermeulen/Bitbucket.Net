using System.Collections.Generic;

namespace Bitbucket.Net.Models.Core.Projects
{
    public class WebHookTestRequest : WebHookRequest
    {
        public string Body { get; set; }
        public List<string> Headers { get; set; }
    }
}