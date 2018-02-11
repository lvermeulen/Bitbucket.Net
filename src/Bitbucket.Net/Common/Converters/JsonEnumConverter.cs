using System;
using Newtonsoft.Json;

namespace Bitbucket.Net.Common.Converters
{
    public abstract class JsonEnumConverter<TEnum> : JsonConverter
        where TEnum: struct, IConvertible
    {
        protected abstract string ConvertToString(TEnum value);

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var actualValue = (TEnum)value;
            writer.WriteValue(ConvertToString(actualValue));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string s = (string)reader.Value;
            return Enum.Parse(typeof(TEnum), s, true);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }
    }
}
