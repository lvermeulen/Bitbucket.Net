namespace Bitbucket.Net.Models.Core.Tasks
{
    public class BitbucketTask : TaskRef
    {
        public TaskAnchor Anchor { get; set; }
        public string State { get; set; }
    }
}
