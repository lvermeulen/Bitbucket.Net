using System.Collections.Generic;

namespace Bitbucket.Net.Models.RefRestrictions
{
    public class RefRestrictionCreate : RefRestrictionBase
    {
        public List<string> Users { get; set; }
        public List<int> AccessKeys { get; set; }
    }
}
