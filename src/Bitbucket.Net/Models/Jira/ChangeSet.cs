using Bitbucket.Net.Models.Core.Projects;

namespace Bitbucket.Net.Models.Jira
{
    public class ChangeSet
{
        public CommitParent FromCommit { get; set; }
        public Commit ToCommit { get; set; }
        public Changes Changes { get; set; }
        public Links Links { get; set; }
        public Repository Repository { get; set; }
    }
}
