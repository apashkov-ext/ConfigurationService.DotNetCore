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
    [Route("api/options")]
    [ApiController]
    public class OptionsController : ControllerBase
    {
        private readonly IOptions _options;

        public OptionsController(IOptions options)
        {
            _options = options;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OptionDto>> Get([FromHeader]string name)
        {
            var options = await _options.Get(name);
            return Ok(options.Select(x => x.ToDto()));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OptionDto>> Get(Guid id)
        {
            var o = await _options.Get(id);
            return Ok(o.ToDto());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<OptionDto>> Create(CreateOptionDto body)
        {
            var o = await _options.Add(body.OptionGroup, body.Name, body.Description, body.Value, body.Type);
            return CreatedAtAction(nameof(Get), new {id = o.Id}, o.ToDto());
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Update(Guid id, UpdateOptionDto body)
        {
            await _options.Update(id, body.Name, body.Description, body.Value, body.Type);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _options.Remove(id);
            return NoContent();
        }
    }
}
