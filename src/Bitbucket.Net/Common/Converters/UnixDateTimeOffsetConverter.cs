using System;
using System.Linq;
using Newtonsoft.Json;

namespace Bitbucket.Net.Common.Converters
{
    public class UnixDateTimeOffsetConverter : JsonConverter
    {
        private static readonly Type[] s_types = { typeof(DateTimeOffset), typeof(long), typeof(int) };

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            string text;

            if (value is DateTimeOffset dateTimeOffset)
            {
                text = dateTimeOffset.ToUnixTimeSeconds().ToString();
            }
            else
            {
                throw new JsonSerializationException($"Unexpected value when converting date. Expected DateTimeOffset, got {value.GetType().Name}.");
            }

            writer.WriteValue(text);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            bool isNullable = TypeExtensions.IsNullableType(objectType);
            if (reader.TokenType == JsonToken.Null)
            {
                if (!isNullable)
                {
                    throw new JsonSerializationException($"Cannot convert null value to {nameof(DateTimeOffset)}.");
                }

                return null;
            }

            var actualType = isNullable
                ? Nullable.GetUnderlyingType(objectType)
                : objectType;

            if (reader.TokenType == JsonToken.Date)
            {
                if (actualType == typeof(DateTimeOffset))
                {
                    return reader.Value is DateTimeOffset 
                        ? reader.Value 
                        : new DateTimeOffset((DateTime)reader.Value);
                }

                if (reader.Value is DateTimeOffset offset)
                {
                    return offset.DateTime;
                }

                return reader.Value;
            }

            string dateText = reader.Value.ToString();

            if (string.IsNullOrEmpty(dateText) && isNullable)
            {
                return null;
            }

            return Convert.ToInt64(dateText).FromUnixTimeSeconds();
        }

        public override bool CanConvert(Type objectType) => s_types.Any(x => x == objectType);
    }
}
