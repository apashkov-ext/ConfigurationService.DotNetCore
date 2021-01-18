using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConfigurationService.Api.Dto;
using ConfigurationService.Api.Extensions;
using ConfigurationService.Application;
using ConfigurationService.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConfigurationService.Api.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjects _projects;
        private readonly IConfigurations _configurations;

        public ProjectsController(IProjects projectsReader, IConfigurations configurations)
        {
            _projects = projectsReader;
            _configurations = configurations;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetProjects()
        {
            var projects = await _projects.Items();
            return Ok(projects.Select(x => x.ToDto()));
        }

        [HttpGet("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProjectDto>> GetProject(string name)
        {
            var project = await _projects.GetItem(name);
            return Ok(project.ToDto());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<CreatedProjectDto>> CreateProject(CreateProjectDto body)
        {
            var created = await _projects.Add(body.Name);
            var dto = created.ToCreatedProjectDto();
            return CreatedAtAction(nameof(GetProject), new { name = dto.Name }, dto);
        }

        [HttpDelete("{name}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteProject(string name)
        {
            await _projects.Remove(name);
            return NoContent();
        }

        [HttpGet("{name}/configs/{env}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<object>> GetConfig(string name, string env, [FromHeader] string apiKey)
        {
            var config = await _configurations.GetItem(name, env, apiKey);
            return Ok(JsObject.Create(config));
        }

        //[HttpPost("{name}/configs")]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        //public async Task<ActionResult<EnvironmentDto>> CreateConfig(string name, CreateConfigDto body)
        //{
        //    var created = await _api.CreateConfig(name, body.Env);
        //    var dto = created.ToDto();
        //    return CreatedAtAction(nameof(GetConfig), new { name, env = dto.Environment }, dto);
        //}

        //[HttpPut("{name}/configs/{env}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult> UpdateConfig(string name, string env, UpdateConfigDto body)
        //{
        //    await _api.UpdateConfig(name, env, body.Content);
        //    return NoContent();
        //}

        //[HttpDelete("{name}/configs/{env}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult> DeleteConfig(string name, string env)
        //{
        //    await _api.DeleteConfig(name, env);
        //    return NoContent();
        //}
    }
}
