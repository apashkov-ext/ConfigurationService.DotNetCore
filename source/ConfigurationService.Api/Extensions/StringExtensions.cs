namespace ConfigurationService.Api.Extensions
{
    internal static class StringExtensions
    {
        public static string ToLowerCamelCase(this string input)
        {
            return string.IsNullOrWhiteSpace(input) ? input : char.ToLower(input[0]) + input.Substring(1);
        }
    }
}
