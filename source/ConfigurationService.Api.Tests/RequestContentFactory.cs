using ConfigurationService.Application;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace ConfigurationService.Api.Tests
{
    internal static class RequestContentFactory
    {
        public static StringContent CreateJsonStringContent(object data)
        {
            var json = JsonSerializer.Serialize(data, SerializerOptions.JsonSerializerOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return content;
        }
    }
}
