﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConfigurationService.Application;
using ConfigurationService.Application.Exceptions;
using ConfigurationService.Domain.Entities;
using ConfigurationService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Environment = ConfigurationService.Domain.Entities.Environment;

namespace ConfigurationService.Persistence
{
    public class Projects : IProjects
    {
        private readonly ConfigurationServiceContext _context;

        public Projects(ConfigurationServiceContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Project>> Items()
        {
            return await _context.Projects.ProjectsWithIncludedEntities().ToListAsync();
        }

        public async Task<Project> GetItem(string name)
        {
            var projName = new ProjectName(name);
            var project = await _context.Projects.ProjectsWithIncludedEntities().FirstOrDefaultAsync(x => x.Name == projName);
            return project ?? throw new NotFoundException("Project does not exist");
        }

        public async Task<Project> Add(string name)
        {
            var projName = new ProjectName(name);
            var existed = await _context.Projects.FirstOrDefaultAsync(x => x.Name.Value == projName.Value);
            if (existed != null)
            {
                throw new AlreadyExistsException("Projects with the same name already exists");
            }

            var newProj = Project.Create(new ProjectName(name), new ApiKey(Guid.NewGuid()), new List<Environment>());
            await _context.Projects.AddAsync(newProj);
            await _context.SaveChangesAsync();

            return newProj;
        }

        public async Task Remove(string name)
        {
            var projName = new ProjectName(name);
            var existed = await _context.Projects.FirstOrDefaultAsync(x => x.Name.Value == projName.Value);
            if (existed == null)
            {
                throw new AlreadyExistsException("Project does not exist");
            }

            _context.Projects.Remove(existed);
            await _context.SaveChangesAsync();
        }
    }
}
