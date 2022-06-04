using System;
using System.Linq;
using System.Text.Json;
using ConfigurationManagementSystem.Domain.Entities;
using ConfigurationManagementSystem.Domain.ValueObjects;

namespace ConfigurationManagementSystem.Persistence.ConfigImporting
{
    internal class OptionGroupDeserializer
    {
        public static OptionGroupEntity DeserializeFromJson(string json)
        {
            var doc = JsonDocument.Parse(json);
            var rootGroup = OptionGroupEntity.Create(new OptionGroupName(""), null);
            FillGroup(rootGroup, doc.RootElement);
            return rootGroup;
        }

        private static void FillGroup(OptionGroupEntity parent, JsonElement element)
        {
            foreach (var p in element.EnumerateObject())
            {
                if (p.Value.ValueKind != JsonValueKind.Object)
                {
                    AddOption(parent, p);
                    continue;
                }
             
                var created = parent.AddNestedGroup(new OptionGroupName(p.Name));
                FillGroup(created, p.Value);
            }
        }

        private static void AddOption(OptionGroupEntity group, JsonProperty prop)
        {
            group.AddOption(new OptionName(prop.Name), GetOptionValue(prop));
        }

        private static OptionValue GetOptionValue(JsonProperty prop)
        {
            return prop.Value.ValueKind switch
            {
                JsonValueKind.Array => GetArrayValue(prop.Value),
                JsonValueKind.String => new OptionValue(prop.Value.GetString()),
                JsonValueKind.False => new OptionValue(false),
                JsonValueKind.True => new OptionValue(true),
                JsonValueKind.Number => new OptionValue(prop.Value.GetInt32()),
                _ => throw new ApplicationException("Unsupported json property value.")
            };
        }

        private static OptionValue GetArrayValue(JsonElement el)
        {
            var arr = el.EnumerateArray();
            if (!arr.Any())
            {
                return null;
            }

            return arr.First().ValueKind switch
            {
                JsonValueKind.String => new OptionValue(arr.Select(x => x.GetString()).ToArray()),
                JsonValueKind.Number => new OptionValue(arr.Select(x => x.GetInt32()).ToArray()),
                _ => throw new ApplicationException("Unsupported json array item value.")
            };
        }
    }
}