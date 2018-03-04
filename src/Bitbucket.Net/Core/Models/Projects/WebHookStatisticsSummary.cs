namespace Bitbucket.Net.Core.Models.Projects
{
    public class WebHookStatisticsSummary
    {
        public WebHookInvocation LastSuccess { get; set; }
        public WebHookInvocation LastFailure { get; set; }
        public WebHookInvocation LastError { get; set; }
        public int Counts { get; set; }
    }
}
