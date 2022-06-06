using System.Text.Json;

namespace ConfigurationManagementSystem.Core;

public static class SerializerOptions
{
    public static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        AllowTrailingCommas = true
    };
}
