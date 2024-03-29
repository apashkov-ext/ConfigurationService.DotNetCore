﻿using System;
using System.Threading.Tasks;
using ConfigurationManagementSystem.Application.Dto;
using ConfigurationManagementSystem.Application.Stories.AddConfigurationStory;
using ConfigurationManagementSystem.Application.Stories.GetConfigurationByIdStory;
using ConfigurationManagementSystem.Application.Stories.GetConfigurationsStory;
using ConfigurationManagementSystem.Application.Stories.RemoveConfigurationStory;
using ConfigurationManagementSystem.Application.Stories.UpdateConfigurationStory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConfigurationManagementSystem.Api.Controllers;

[ApiController]
[Route("api/configurations")]
[Produces("application/json")]
public class ConfigurationsController : ControllerBase
{
    private readonly GetConfigurationByIdStory _getConfigurationByIdStory;
    private readonly AddConfigurationStory _addConfigurationStory;
    private readonly UpdateConfigurationStory _updateConfigurationStory;
    private readonly RemoveConfigurationStory _removeConfigurationStory;
    private readonly FindConfigurationsByNameStory _getConfigurationsStory;

    public ConfigurationsController(FindConfigurationsByNameStory getConfigurationsStory,
        GetConfigurationByIdStory getConfigurationByIdStory,
        AddConfigurationStory addConfigurationStory,
        UpdateConfigurationStory updateConfigurationStory,
        RemoveConfigurationStory removeConfigurationStory)
    {
        _getConfigurationByIdStory = getConfigurationByIdStory;
        _addConfigurationStory = addConfigurationStory;
        _updateConfigurationStory = updateConfigurationStory;
        _removeConfigurationStory = removeConfigurationStory;
        _getConfigurationsStory = getConfigurationsStory;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedResponseDto<ConfigurationDto>>> Get([FromQuery] PagedRequest request)
    {
        return Ok(await _getConfigurationsStory.ExecuteAsync(request));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ConfigurationDto>> Get(Guid id)
    {
        return Ok(await _getConfigurationByIdStory.ExecuteAsync(id));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<ConfigurationDto>> Create(CreateConfigurationDto body)
    {
        var created = await _addConfigurationStory.ExecuteAsync(body);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ConfigurationDto>> Update(Guid id, UpdateConfigurationDto body)
    {
        await _updateConfigurationStory.ExecuteAsync(id, body.Name);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _removeConfigurationStory.ExecuteAsync(id);
        return NoContent();
    }
}
