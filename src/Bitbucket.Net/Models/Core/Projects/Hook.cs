namespace Bitbucket.Net.Models.Projects
{
    public class Hook
    {
        public HookDetails Details { get; set; }
        public bool Enabled { get; set; }
        public bool Configured { get; set; }
        public HookScope Scope { get; set; }
    }
}
