using System;
using Bitbucket.Net.Models.Projects;
using Newtonsoft.Json;

namespace Bitbucket.Net.Common
{
    public class PullRequestStatesConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var pullRequestState = (PullRequestStates)value;
            writer.WriteValue(BitbucketHelpers.PullRequestStateToString(pullRequestState));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string pullRequestStateString = (string)reader.Value;
            return Enum.Parse(typeof(PullRequestStates), pullRequestStateString, true);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }
    }
}
