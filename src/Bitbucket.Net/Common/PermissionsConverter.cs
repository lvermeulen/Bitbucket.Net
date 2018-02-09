using System;
using Bitbucket.Net.Models.Projects;
using Newtonsoft.Json;

namespace Bitbucket.Net.Common
{
    public class PermissionsConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var permission = (Permissions)value;
            writer.WriteValue(BitbucketHelpers.PermissionToString(permission));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string permissionString = (string)reader.Value;
            return Enum.Parse(typeof(Permissions), permissionString, true);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }
    }
}
