using System.Text.RegularExpressions;

namespace ConfigurationService.Application.Sources.GitHub
{
    internal class ContentFileNameMatcher
    {
        private readonly string _filemask;
        private readonly Regex _regexp;

        public ContentFileNameMatcher(string filemask)
        {
            _filemask = filemask;
            _regexp = new Regex($"^{filemask}(.(?<env>[a-zA-Z]+))?.json");
        }

        public bool IsCorrectName(string name) {
            return _regexp.IsMatch(name);
        }

        public string ExtractEnv(string name) {
            return _regexp.Match(name).Groups["env"].Value;
        }
        
        public string BuildFileName(string env = null)
        {
            return string.IsNullOrWhiteSpace(env) ? $"{_filemask}.json" : $"{_filemask}.{env}.json";
        }
    }
}
