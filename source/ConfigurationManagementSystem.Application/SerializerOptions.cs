using System.Text.Json;

namespace ConfigurationManagementSystem.Application
{
    public static class SerializerOptions
    {
        public static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            AllowTrailingCommas = true
        };
    }
}
