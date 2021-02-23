using System;
using System.Linq;
using System.Text.Json;
using ConfigurationService.Domain.Entities;
using ConfigurationService.Domain.ValueObjects;

namespace ConfigurationService.Persistence.ConfigImporting
{
    internal class OptionGroupHierarchyImporter
    {
        public OptionGroup ImportFromJson(JsonDocument json)
        {
            var rootGroup = OptionGroup.Create(new OptionGroupName(""), new Description(""), null);
            FillGroup(rootGroup, json.RootElement);
            return rootGroup;
        }

        private static void FillGroup(OptionGroup parent, JsonElement element)
        {
            foreach (var p in element.EnumerateObject())
            {
                if (p.Value.ValueKind == JsonValueKind.Object)
                {
                    var created = parent.AddNestedGroup(new OptionGroupName(p.Name), new Description(""));
                    FillGroup(created, p.Value);
                }
                else
                {
                    AddOption(parent, p);
                }
            }
        }

        private static void AddOption(OptionGroup group, JsonProperty prop)
        {
            group.AddOption(new OptionName(prop.Name), new Description(""), GetOptionValue(prop));
        }

        private static OptionValue GetOptionValue(JsonProperty prop)
        {
            return prop.Value.ValueKind switch
            {
                JsonValueKind.Array => GetArrayValue(prop.Value),
                JsonValueKind.String => new OptionValue(prop.Value.GetString()),
                JsonValueKind.False => new OptionValue(prop.Value.GetBoolean()),
                JsonValueKind.True => new OptionValue(prop.Value.GetBoolean()),
                JsonValueKind.Number => new OptionValue(prop.Value.GetInt32()),
                _ => throw new ApplicationException("Invalid Json format")
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
                _ => throw new ApplicationException("Invalid Json format")
            };
        }
    }
}