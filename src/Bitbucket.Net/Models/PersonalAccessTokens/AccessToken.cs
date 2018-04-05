using System;
using Bitbucket.Net.Common.Converters;
using Bitbucket.Net.Models.Core.Users;
using Newtonsoft.Json;

namespace Bitbucket.Net.Models.PersonalAccessTokens
{
    public class AccessToken : AccessTokenCreate
    {
        public string Id { get; set; }
        [JsonConverter(typeof(UnixDateTimeOffsetConverter))]
        public DateTimeOffset CreatedDate { get; set; }
        [JsonConverter(typeof(UnixDateTimeOffsetConverter))]
        public DateTimeOffset LastAuthenticated { get; set; }
        public User User { get; set; }
    }
}
