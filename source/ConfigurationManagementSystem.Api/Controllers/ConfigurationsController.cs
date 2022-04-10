using System;
using System.Threading.Tasks;
using ConfigurationManagementSystem.Api.Dto;
using ConfigurationManagementSystem.Api.Extensions;
using ConfigurationManagementSystem.Application;
using ConfigurationManagementSystem.Application.Pagination;
using ConfigurationManagementSystem.Application.Stories.GetConfigurationsStory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConfigurationManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("api/configurations")]
    [Produces("application/json")]
    public class ConfigurationsController : ControllerBase
    {
        private readonly IEnvironments _environments;
        private readonly GetConfigurationsStory _getConfigurationsStory;

        public ConfigurationsController(IEnvironments environments,
            GetConfigurationsStory getConfigurationsStory)
        {
            _environments = environments;
            _getConfigurationsStory = getConfigurationsStory;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResponseDto<ConfigurationDto>>> Get([FromQuery] GetRequestOptions options)
        {
            var pOpt = PaginationOptions.Create(options.Offset, options.Limit);
            var configs = await _getConfigurationsStory.ExecuteAsync(options.Name, pOpt, options.Hierarchy ?? false);
            var result = configs.ToPagedResponseDto(ConfigurationEntityExtensions.ToDto);

            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ConfigurationDto>> Get(Guid id)
        {
            var env = await _environments.GetAsync(id);
            return Ok(env.ToDto());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<ConfigurationDto>> Create(CreateConfigurationDto body)
        {
            var created = await _environments.AddAsync(body.Application, body.Name);
            var dto = created.ToDto();
            return CreatedAtAction(nameof(Get), new { envId = created.Id }, dto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ConfigurationDto>> Update(Guid id, UpdateConfigurationDto body)
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
