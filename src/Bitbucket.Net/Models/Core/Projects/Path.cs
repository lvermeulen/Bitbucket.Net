using System.Collections.Generic;

namespace Bitbucket.Net.Models.Core.Projects
{
    public class Path
    {
        public List<string> Components { get; set; }
        public string Parent { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        // ReSharper disable once InconsistentNaming
        public string toString { get; set; }
    }
}