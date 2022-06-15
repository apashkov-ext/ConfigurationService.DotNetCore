using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConfigurationManagementSystem.Application.Dto;
using ConfigurationManagementSystem.Application.Stories.AddOptionGroupStory;
using ConfigurationManagementSystem.Application.Stories.FindOptionGroupsByNameStory;
using ConfigurationManagementSystem.Application.Stories.GetOptionGroupByIdStory;
using ConfigurationManagementSystem.Application.Stories.RemoveOptionGroupStory;
using ConfigurationManagementSystem.Application.Stories.UpdateOptionGroupStory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConfigurationManagementSystem.Api.Controllers;

[Route("api/option-groups")]
[ApiController]
public class OptionGroupsController : ControllerBase
{
    private readonly FindOptionGroupsByNameStory _findOptionGroupsByNameStory;
    private readonly GetOptionGroupByIdStory _getOptionGroupByIdStory;
    private readonly AddOptionGroupStory _addOptionGroupStory;
    private readonly UpdateOptionGroupStory _updateOptionGroupStory;
    private readonly RemoveOptionGroupStory _removeOptionGroupStory;

    public OptionGroupsController(FindOptionGroupsByNameStory findOptionGroupsByNameStory,
        GetOptionGroupByIdStory getOptionGroupByIdStory,
        AddOptionGroupStory addOptionGroupStory,
        UpdateOptionGroupStory updateOptionGroupStory,
        RemoveOptionGroupStory removeOptionGroupStory)
    {
        _findOptionGroupsByNameStory = findOptionGroupsByNameStory;
        _getOptionGroupByIdStory = getOptionGroupByIdStory;
        _addOptionGroupStory = addOptionGroupStory;
        _updateOptionGroupStory = updateOptionGroupStory;
        _removeOptionGroupStory = removeOptionGroupStory;
    }

    [HttpGet]
    [Produces(typeof(IEnumerable<OptionGroupDto>))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<OptionGroupDto>>> Get([FromQuery] PagedRequest request)
    {
        return Ok(await _findOptionGroupsByNameStory.ExecuteAsync(request));
    }

    [HttpGet("{id}")]
    [Produces(typeof(OptionGroupDto))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OptionGroupDto>> Get(Guid id)
    {
        return Ok(await _getOptionGroupByIdStory.ExecuteAsync(id));
    }

    [HttpPost]
    [Produces(typeof(OptionGroupDto))]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<OptionGroupDto>> Create(CreateOptionGroupDto body)
    {
        var group = await _addOptionGroupStory.ExecuteAsync(body);
        return CreatedAtAction(nameof(Get), new { id = group.Id }, group);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Update(Guid id, UpdateOptionGroupDto body)
    {
        await _updateOptionGroupStory.ExecuteAsync(id, body);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _removeOptionGroupStory.ExecuteAsync(id);
        return NoContent();
    }
}
