﻿using System;
using System.Threading.Tasks;
using ConfigurationService.Api.Dto;
using ConfigurationService.Api.Extensions;
using ConfigurationService.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConfigurationService.Api.Controllers
{
    [Route("api/option-groups")]
    public class OptionGroupsController : ControllerBase
    {
        private readonly IOptionGroups _optionGroups;

        public OptionGroupsController(IOptionGroups optionGroups)
        {
            _optionGroups = optionGroups;
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
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<OptionGroupDto>> Create(CreateOptionGroupDto request)
        {
            var group = await _optionGroups.Add(request.Parent, request.Name, request.Description);
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
