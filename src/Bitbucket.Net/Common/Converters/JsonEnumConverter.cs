using System;
using Newtonsoft.Json;

namespace Bitbucket.Net.Common.Converters
{
    public abstract class JsonEnumConverter<TEnum> : JsonConverter
        where TEnum: struct, IConvertible
    {
        protected abstract string ConvertToString(TEnum value);

        protected abstract TEnum ConvertFromString(string s);

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var actualValue = (TEnum)value;
            writer.WriteValue(ConvertToString(actualValue));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string s = (string)reader.Value;
            return ConvertFromString(s);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }
    }
}
