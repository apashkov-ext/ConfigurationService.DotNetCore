using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigurationService.Application;
using ConfigurationService.Application.Exceptions;
using ConfigurationService.Domain;
using ConfigurationService.Domain.Entities;
using ConfigurationService.Domain.ValueObjects;
using ConfigurationService.Persistence.ConfigImporting;
using ConfigurationService.Persistence.ConfigImporting.Abstractions;
using ConfigurationService.Persistence.ConfigImporting.Implementation;
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

            var originalTree = new ConfigTree(env.GetRootOptionGroop(), _context);
            var json = await GetJsonString(file);
            var parsedTree = ParseTree(json);

            originalTree.ReplaceNodes(parsedTree);
            await _context.SaveChangesAsync();
        }

        private static async Task<string> GetJsonString(byte[] file)
        {
            await using var stream = new MemoryStream(file);
            using var reader = new StreamReader(stream, Encoding.UTF8);
            return await reader.ReadToEndAsync();
        }

        private static Tree<OptionGroup> ParseTree(string json)
        {
            var hierarchy = new OptionGroupHierarchyImporter().ImportFromJson(json);
            return new ImportedTree(hierarchy);
        }
    }
}
