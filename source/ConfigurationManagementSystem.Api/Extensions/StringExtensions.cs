namespace ConfigurationManagementSystem.Api.Extensions
{
    internal static class StringExtensions
    {
        public static string ToLowerCamelCase(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return input;
            }

            var modified = char.ToLower(input[0]) + input.Substring(1);
            return modified;
        }
    }
}
