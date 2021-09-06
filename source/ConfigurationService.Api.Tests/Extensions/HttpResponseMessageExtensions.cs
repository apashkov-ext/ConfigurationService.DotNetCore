using ConfigurationService.Application;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConfigurationService.Api.Tests.Extensions
{
    internal static class HttpResponseMessageExtensions
    {
        public static async Task<T> ParseContentAsync<T>(this HttpResponseMessage response)
        {
            var json = await response.Content.ReadAsStringAsync();
            var dto = JsonSerializer.Deserialize<T>(json, SerializerOptions.JsonSerializerOptions);
            return dto;
        }
    }
}
