using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ConfigurationService.Application;
using ConfigurationService.Application.Exceptions;
using ConfigurationService.Domain;
using ConfigurationService.Domain.Entities;
using ConfigurationService.Domain.ValueObjects;
using ConfigurationService.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ConfigurationService.Persistence
{
    public class Configurations : IConfigurations
    {
        private readonly ConfigurationServiceContext _context;

        public Configurations(ConfigurationServiceContext context)
        {
            _context = context;
        }

        public async Task<OptionGroup> GetItem(string project, string environment, string apiKey)
        {
            var projName = new ProjectName(project);
            var envName = new EnvironmentName(environment);
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new NotFoundException("Project does not exist");
            }
            var key = new ApiKey(apiKey);

            var proj = await _context.Projects.ProjectsWithIncludedEntities().FirstOrDefaultAsync(x => x.Name.Value == projName.Value);
            if (proj == null || proj.ApiKey.Value != key.Value)
            {
                throw new NotFoundException("Project does not exist");
            }

            var env = proj.Environments.FirstOrDefault(x => x.Name.Value == envName.Value);
            if (env == null)
            {
                throw new NotFoundException("Configuration does not exist");
            }

            return env.GetRootOptionGroop();
        }

        public async Task Import(Guid project, string environment, byte[] file)
        {
            var envName = new EnvironmentName(environment);
            var proj = await _context.Projects.ProjectsWithIncludedEntities().FirstOrDefaultAsync(x => x.Id == project);
            if (proj == null)
            {
                throw new NotFoundException("Project does not exist");
            }

            var env = proj.Environments.FirstOrDefault(x => x.Name.Value == envName.Value);
            if (env == null)
            {
                throw new NotFoundException("Configuration does not exist");
            }

            var json = await GetJsonString(file);
            
        }

        private static async Task<string> GetJsonString(byte[] file)
        {
            await using var stream = new MemoryStream(file);
            using var reader = new StreamReader(stream, Encoding.UTF8);
            return await reader.ReadToEndAsync();
        }
    }

    public class ConfigImportingReport
    {

    }

    public class ConfigImporter
    {
        private readonly ConfigurationServiceContext _context;

        public ConfigImporter(ConfigurationServiceContext context)
        {
            
            _context = context;
        }

        public ConfigImportingReport Import(OptionGroup root, string json)
        {
            var doc = JsonDocument.Parse(json);
            var result = new ConfigTreeUpdater(root).Update(doc);
            var allGroups = root.WithChildren().ToList();
            var notVisitedGroups = allGroups.Except(result.VisitedGroups);
            var notVisitedOptions = allGroups.Except(notVisitedGroups).SelectMany(x => x.Options).Except(result.VisitedOptions);
        }
    }

    internal class ConfigTreeUpdatingResult
    {
        public IEnumerable<OptionGroup> VisitedGroups { get; }
        public IEnumerable<Option> VisitedOptions { get; }
        public IEnumerable<string> Errors { get; }

        public ConfigTreeUpdatingResult(IEnumerable<OptionGroup> visitedGroups, IEnumerable<Option> visitedOptions, IEnumerable<string> errors)
        {
            VisitedGroups = visitedGroups;
            VisitedOptions = visitedOptions;
            Errors = errors;
        }
    }

    internal class ConfigTreeUpdater
    {
        private readonly OptionGroup _root;
        private readonly List<OptionGroup> _visitedGroups = new List<OptionGroup>();
        private readonly List<Option> _visitedOptions = new List<Option>();
        private readonly List<string> _errors = new List<string>();

        public ConfigTreeUpdater(OptionGroup root)
        {
            _root = root;
        }

        public ConfigTreeUpdatingResult Update(JsonDocument doc)
        {
            _visitedGroups.Clear();
            _visitedOptions.Clear();

            UpdateGroup(_root, doc.RootElement);
            return new ConfigTreeUpdatingResult(_visitedGroups, _visitedOptions, _errors);
        }

        private void UpdateGroup(OptionGroup group, JsonElement el)
        {
            _visitedGroups.Add(group);

            foreach (var prop in el.EnumerateObject())
            {
                try
                {
                    switch (prop.Value.ValueKind)
                    {
                        case JsonValueKind.Object:
                            UpdateOrCreateGroup(group, prop);
                            break;
                    
                        default:
                            UpdateOrCreateOption(group, prop);
                            break;
                    }
                }
                catch (Exception e)
                {
                    _errors.Add(e.Message);
                }
            }
        }

        private void UpdateOrCreateGroup(OptionGroup group, JsonProperty prop)
        {
            var nested = group.NestedGroups.FirstOrDefault(x => x.Name.Value.Equals(prop.Name, StringComparison.InvariantCultureIgnoreCase));
            if (nested != null)
            {
                UpdateGroup(nested, prop.Value);
            }
            else
            {
                CreateGroup(group, prop);
            }
        }

        private void UpdateOrCreateOption(OptionGroup group, JsonProperty prop)
        {
            var o = group.Options.FirstOrDefault(x => x.Name.Value.Equals(prop.Name, StringComparison.InvariantCultureIgnoreCase));
            if (o != null)
            {
                UpdateOption(o, prop);
            }
            else
            {
                CreateOption(group, prop);
            }
        }

        private void CreateGroup(OptionGroup parent, JsonProperty prop)
        {
            var created = parent.AddNestedGroup(new OptionGroupName(prop.Name), new Description(""));
            _visitedGroups.Add(created);

            foreach (var p in prop.Value.EnumerateObject())
            {
                CreateOption(created, p);
            }
        }

        private void UpdateOption(Option option, JsonProperty prop)
        {
            _visitedOptions.Add(option);
            option.UpdateValue(GetOptionValue(prop));
        }

        private void CreateOption(OptionGroup group, JsonProperty prop)
        {
            var opt = group.AddOption(new OptionName(prop.Name), new Description(""), GetOptionValue(prop));
            _visitedOptions.Add(opt);
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
