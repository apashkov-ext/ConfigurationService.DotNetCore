using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConfigurationManagementSystem.Application.Dto;
using ConfigurationManagementSystem.Application.Stories.AddOptionStory;
using ConfigurationManagementSystem.Application.Stories.FindOptionsByNameStory;
using ConfigurationManagementSystem.Application.Stories.GetOptionByIdStory;
using ConfigurationManagementSystem.Application.Stories.RemoveOptionStory;
using ConfigurationManagementSystem.Application.Stories.UpdateOptionStory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConfigurationManagementSystem.Api.Controllers;

[Route("api/options")]
[ApiController]
public class OptionsController : ControllerBase
{
    private readonly FindOptionsByNameStory _findOptionsByNameStory;
    private readonly GetOptionByIdStory _getOptionByIdStory;
    private readonly AddOptionStory _addOptionStory;
    private readonly UpdateOptionStory _updateOptionStory;
    private readonly RemoveOptionStory _removeOptionStory;

    public OptionsController(FindOptionsByNameStory findOptionsByNameStory,
        GetOptionByIdStory getOptionByIdStory,
        AddOptionStory addOptionStory,
        UpdateOptionStory updateOptionStory,
        RemoveOptionStory removeOptionStory)
    {
        _findOptionsByNameStory = findOptionsByNameStory;
        _getOptionByIdStory = getOptionByIdStory;
        _addOptionStory = addOptionStory;
        _updateOptionStory = updateOptionStory;
        _removeOptionStory = removeOptionStory;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<OptionDto>>> Get([FromQuery] PagedRequest request)
    {
        return Ok(await _findOptionsByNameStory.ExecuteAsync(request));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OptionDto>> Get(Guid id)
    {
        return Ok(await _getOptionByIdStory.ExecuteAsync(id));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<OptionDto>> Create(CreateOptionDto body)
    {
        var created = await _addOptionStory.ExecuteAsync(body);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Update(Guid id, UpdateOptionDto body)
    {
        await _updateOptionStory.ExecuteAsync(id, body);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _removeOptionStory.ExecuteAsync(id);
        return NoContent();
    }
}
