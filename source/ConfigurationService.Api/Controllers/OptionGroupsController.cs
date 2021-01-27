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
    [Route("api/option-groups")]
    [ApiController]
    public class OptionGroupsController : ControllerBase
    {
        private readonly IOptionGroups _optionGroups;

        public OptionGroupsController(IOptionGroups optionGroups)
        {
            _optionGroups = optionGroups;
        }

        [HttpGet("{name?}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<OptionGroupDto>> Get(string name)
        {
            var groups = (await _optionGroups.Get(name)).Where(x => x.Parent == null);
            return Ok(groups.Select(x => x.ToDto()));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OptionGroupDto>> Get(Guid id)
        {
            var group = await _optionGroups.Get(id);
            return Ok(group.ToDto());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<OptionGroupDto>> Create(CreateOptionGroupDto body)
        {
            var group = await _optionGroups.Add(body.Parent, body.Name, body.Description);
            var dto = group.ToDto();
            return CreatedAtAction(nameof(Get), new {id = group.Id}, dto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update(Guid id, UpdateOptionGroupDto body)
        {
            await _optionGroups.Update(id, body.Name, body.Description);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _optionGroups.Remove(id);
            return NoContent();
        }
    }
}
