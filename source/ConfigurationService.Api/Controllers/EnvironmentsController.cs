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
    [ApiController]
    [Route("api/environments")]
    [Produces("application/json")]
    public class EnvironmentsController : ControllerBase
    {
        private readonly IEnvironments _environments;

        public EnvironmentsController(IEnvironments environments)
        {
            _environments = environments;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<EnvironmentDto>>> Get(string name)
        {
            var envs = await _environments.GetAsync(name);
            return Ok(envs.Select(x => x.ToDto()));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EnvironmentDto>> Get(Guid id)
        {
            var env = await _environments.GetAsync(id);
            return Ok(env.ToDto());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<EnvironmentDto>> Create(CreateEnvDto body)
        {
            var created = await _environments.AddAsync(body.Project, body.Name);
            var dto = created.ToDto();
            return CreatedAtAction(nameof(Get), new { envId = created.Id }, dto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EnvironmentDto>> Update(Guid id, UpdateEnvDto body)
        {
            await _environments.UpdateAsync(id, body.Name);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _environments.RemoveAsync(id);
            return NoContent();
        }
    }
}
