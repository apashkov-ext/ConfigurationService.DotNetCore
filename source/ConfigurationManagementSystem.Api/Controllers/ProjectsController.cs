using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConfigurationManagementSystem.Api.Dto;
using ConfigurationManagementSystem.Api.Extensions;
using ConfigurationManagementSystem.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConfigurationManagementSystem.Api.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjects _projects;

        public ProjectsController(IProjects projectsReader)
        {
            _projects = projectsReader;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> Get(string name)
        {
            var projects = await _projects.GetAsync(name);
            var result = projects.Select(x => x.ToDto()).ToList();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProjectDto>> Get(Guid id)
        {
            var project = await _projects.GetAsync(id);
            return Ok(project.ToDto());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<CreatedProjectDto>> Create(CreateProjectDto body)
        {
            var created = await _projects.AddAsync(body.Name);
            var dto = created.ToCreatedProjectDto();
            return CreatedAtAction(nameof(Get), new { id = created.Id }, dto);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _projects.RemoveAsync(id);
            return NoContent();
        }
    }
}
