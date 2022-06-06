using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ConfigurationManagementSystem.Core;

namespace ConfigurationManagementSystem.Api.Tests.Extensions;

internal static class HttpResponseMessageExtensions
{
    public static async Task<T> ParseContentAsync<T>(this HttpResponseMessage response)
    {
        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(json, SerializerOptions.JsonSerializerOptions);
    }
}
