using System;
using System.IO;
using System.Threading.Tasks;
using ConfigurationManagementSystem.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConfigurationManagementSystem.Api.Controllers
{
    [Route("api/configurations")]
    [ApiController]
    public class ConfigurationsController : ControllerBase
    {
        private readonly IConfigurations _configurations;

        public ConfigurationsController(IConfigurations configurations)
        {
            _configurations = configurations;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<object>> GetConfig(string projName, string envName, [FromHeader] string apiKey)
        {
            var config = await _configurations.GetItem(projName, envName, apiKey);
            var response = JsObject.Create(config);
            return Ok(response);
        }

        [HttpPost]
        [RequestSizeLimit(10 * 1024 * 1024)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Import(Guid projId, string envName)
        {
            var file = Request.Form.Files[0];
            if (file.Length <= 0)
            {
                return BadRequest();
            }

            await using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            await _configurations.Import(projId, envName, stream.ToArray());

            return Ok();
        }
    }
}
