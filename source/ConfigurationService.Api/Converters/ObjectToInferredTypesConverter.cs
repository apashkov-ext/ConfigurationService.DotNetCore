using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using ConfigurationService.Application;

namespace ConfigurationService.Api.Converters
{
    public class ObjectToInferredTypesConverter : JsonConverter<object>
    {
        public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.TokenType switch 
            {
                JsonTokenType.True => true,
                JsonTokenType.False => false,
                JsonTokenType.Number => reader.GetInt32(),
                JsonTokenType.String => reader.GetString(),
                JsonTokenType.StartArray => GetArray(reader),
                _ => throw new ApplicationException("Invalid json type")
            };
        }

        public override void Write(
            Utf8JsonWriter writer,
            object objectToWrite,
            JsonSerializerOptions options) =>
            throw new InvalidOperationException("Should not get here.");

        private static object[] GetArray(Utf8JsonReader reader)
        {
            var list = new List<object>();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndArray)
                {
                    break;
                }
                list.Add(JsonSerializer.Deserialize<object>(ref reader, SerializerOptions.JsonSerializerOptions));
            }

            return list.ToArray();
        }
    }
}
