using System.Collections.Generic;
using Bitbucket.Net.Models.Core.Projects;

namespace Bitbucket.Net.Models.Jira
{
    public class Changes
    {
        public int Size { get; set; }
        public int Limit { get; set; }
        public bool IsLastPage { get; set; }
        public List<Change> Values { get; set; }
        public int Start { get; set; }
    }
}