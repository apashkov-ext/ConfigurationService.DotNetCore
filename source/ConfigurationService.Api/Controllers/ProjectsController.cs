using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConfigurationService.Api.Dto;
using ConfigurationService.Api.Extensions;
using ConfigurationService.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConfigurationService.Api.Controllers
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
        public async Task<ActionResult<IEnumerable<ProjectDto>>> Get([FromHeader]string name)
        {
            var projects = await _projects.Get(name);
            return Ok(projects.Select(x => x.ToDto()));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProjectDto>> Get(Guid id)
        {
            var project = await _projects.Get(id);
            return Ok(project.ToDto());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<CreatedProjectDto>> Create(CreateProjectDto body)
        {
            var created = await _projects.Add(body.Name);
            var dto = created.ToCreatedProjectDto();
            return CreatedAtAction(nameof(Get), new { id = created.Id }, dto);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _projects.Remove(id);
            return NoContent();
        }
    }
}
