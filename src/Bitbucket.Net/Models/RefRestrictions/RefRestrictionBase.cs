using System.Collections.Generic;
using Bitbucket.Net.Common.Converters;
using Bitbucket.Net.Models.DefaultReviewers;
using Newtonsoft.Json;

namespace Bitbucket.Net.Models.RefRestrictions
{
    public abstract class RefRestrictionBase
    {
        [JsonConverter(typeof(RefRestrictionTypesConverter))]
        public RefRestrictionTypes Type { get; set; }
        public RefMatcher Matcher { get; set; }
        public List<string> Groups { get; set; }
    }
}