using System;
using System.Linq;
using System.Threading.Tasks;
using ConfigurationService.Api.Dto;
using ConfigurationService.Api.Extensions;
using ConfigurationService.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConfigurationService.Api.Controllers
{
    [Route("api/environments")]
    [ApiController]
    public class EnvironmentsController : ControllerBase
    {
        private readonly IEnvironments _environments;

        public EnvironmentsController(IEnvironments environments)
        {
            _environments = environments;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<EnvironmentDto>> Get()
        {
            var envs = await _environments.Get();
            return Ok(envs.Select(x => x.ToDto()));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EnvironmentDto>> Get(Guid id)
        {
            var env = await _environments.Get(id);
            return Ok(env.ToDto());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<EnvironmentDto>> Create(CreateEnvDto body)
        {
            var created = await _environments.Add(body.Project, body.Name);
            var dto = created.ToDto();
            return CreatedAtAction(nameof(Get), new { envId = created.Id }, dto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EnvironmentDto>> Update(Guid id, UpdateEnvDto body)
        {
            await _environments.Update(id, body.Name);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _environments.Remove(id);
            return NoContent();
        }
    }
}
