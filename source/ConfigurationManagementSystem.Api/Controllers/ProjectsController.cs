using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConfigurationManagementSystem.Api.Dto;
using ConfigurationManagementSystem.Api.Extensions;
using ConfigurationManagementSystem.Application;
using ConfigurationManagementSystem.Application.Pagination;
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
        public async Task<ActionResult<PagedResponseDto<ProjectDto>>> Get([FromQuery] GetRequestOptions options)
        {
            var pOpt = new PaginationOptions(options.Offset ?? 0, options.Limit ?? 20);
            var projects = await _projects.GetAsync(options.Name, pOpt);
            var result = projects.ToPagedResponseDto(ProjectExtensions.ToDto);

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
