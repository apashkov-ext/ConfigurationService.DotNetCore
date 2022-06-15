using System;
using System.Threading.Tasks;
using ConfigurationManagementSystem.Application.Dto;
using ConfigurationManagementSystem.Application.Stories.AddApplicationStory;
using ConfigurationManagementSystem.Application.Stories.GetApplicationByIdStory;
using ConfigurationManagementSystem.Application.Stories.GetApplicationsStory;
using ConfigurationManagementSystem.Application.Stories.RemoveApplicationStory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConfigurationManagementSystem.Api.Controllers;

[Route("api/applications")]
[ApiController]
public class ApplicationsController : ControllerBase
{
    private readonly FindApplicationsByNameStory _getApplicationsStory;
    private readonly GetApplicationByIdStory _getApplicationByIdStory;
    private readonly AddApplicationStory _addApplicationStory;
    private readonly RemoveApplicationStory _removeApplicationStory;

    public ApplicationsController(FindApplicationsByNameStory getApplicationsStory,
        GetApplicationByIdStory getApplicationByIdStory,
        AddApplicationStory addApplicationStory,
        RemoveApplicationStory removeApplicationStory)
    {
        _getApplicationsStory = getApplicationsStory;
        _getApplicationByIdStory = getApplicationByIdStory;
        _addApplicationStory = addApplicationStory;
        _removeApplicationStory = removeApplicationStory;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedResponseDto<ApplicationDto>>> Get([FromQuery] PagedRequest request)
    {
        return Ok(await _getApplicationsStory.ExecuteAsync(request));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApplicationDto>> Get(Guid id)
    {
        return Ok(await _getApplicationByIdStory.ExecuteAsync(id));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<CreatedApplicationDto>> Create(CreateApplicationDto body)
    {
        var created = await _addApplicationStory.ExecuteAsync(body.Name);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _removeApplicationStory.ExecuteAsync(id);
        return NoContent();
    }
}
