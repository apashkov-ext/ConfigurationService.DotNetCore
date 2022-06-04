namespace ConfigurationManagementSystem.Core
{
    public static class ApplicationConstants
    {
        public readonly static string EnvironmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "localhost";
    }
}