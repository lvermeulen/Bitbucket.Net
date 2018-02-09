using System.Collections.Generic;

namespace Bitbucket.Net.Models.Common
{
    public class BitbucketResult<T>
    {
        public int Size { get; set; }
        public int Limit { get; set; }
        public bool IsLastPage { get; set; }
        public List<T> Values { get; set; }
        public int Start { get; set; }
        public int NextPageStart { get; set; }
    }
}
