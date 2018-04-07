using Bitbucket.Net.Models.Core.Projects;

namespace Bitbucket.Net.Models.Ssh
{
    public class RepositoryKey : KeyBase
    {
        public Repository Repository { get; set; }
    }
}