using Bitbucket.Net.Models.Core.Projects;

namespace Bitbucket.Net.Models.Ssh
{
    public class ProjectKey : KeyBase
    {
        public Project Project { get; set; }
    }
}
