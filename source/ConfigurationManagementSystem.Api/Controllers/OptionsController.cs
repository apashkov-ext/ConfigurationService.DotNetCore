﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConfigurationManagementSystem.Api.Dto;
using ConfigurationManagementSystem.Api.Extensions;
using ConfigurationManagementSystem.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConfigurationManagementSystem.Api.Controllers;

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
    public async Task<ActionResult<IEnumerable<OptionDto>>> Get(string name)
    {
        var options = await _options.GetAsync(name);
        var result = options.Select(x => x.ToDto());
        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OptionDto>> Get(Guid id)
    {
        var o = await _options.GetAsync(id);
        return Ok(o.ToDto());
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<OptionDto>> Create(CreateOptionDto body)
    {
        var o = await _options.AddAsync(body.OptionGroup, body.Name, body.Value, body.Type);
        return CreatedAtAction(nameof(Get), new { id = o.Id }, o.ToDto());
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Update(Guid id, UpdateOptionDto body)
    {
        await _options.UpdateAsync(id, body.Name, body.Value);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _options.RemoveAsync(id);
        return NoContent();
    }
}
