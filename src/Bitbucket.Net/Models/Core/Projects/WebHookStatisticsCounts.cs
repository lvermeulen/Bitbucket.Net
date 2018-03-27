namespace Bitbucket.Net.Models.Core.Projects
{
    public class WebHookStatisticsCounts
    {
        public int Errors { get; set; }
        public int Failures { get; set; }
        public int Successes { get; set; }
        public TimeWindow Window { get; set; }
    }
}