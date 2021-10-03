using System;
using System.Text.Json;
using ConfigurationManagementSystem.Domain.Entities;

namespace ConfigurationManagementSystem.Api
{
    public class JsonValueParser
    {
        private readonly JsonElement _element;
        private readonly OptionValueType _type;

        public JsonValueParser(object valueKind, OptionValueType type)
        {
            if (!(valueKind is JsonElement element)) throw new ApplicationException("Invalid json format");
            _element = element;
            _type = type;
        }

        public object Parse()
        {
            return _element.ValueKind switch
            {
                JsonValueKind.Array => DeserializeArray(_element.GetRawText()),
                JsonValueKind.String => _element.GetString(),
                JsonValueKind.True => _element.GetBoolean(),
                JsonValueKind.False => _element.GetBoolean(),
                JsonValueKind.Number => _element.GetInt32(),
                _ => throw new ApplicationException("Invalid json format")
            };
        }

        private object DeserializeArray(string raw)
        {
            return _type switch
            {
                OptionValueType.StringArray => JsonSerializer.Deserialize<string[]>(raw),
                OptionValueType.NumberArray => JsonSerializer.Deserialize<int[]>(raw),
                _ => throw new ApplicationException("Invalid json format")
            };
        }
    }
}
