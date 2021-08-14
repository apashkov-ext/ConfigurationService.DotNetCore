using System.Threading.Tasks;
using ConfigurationService.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConfigurationService.Api.Controllers
{
    [Route("api/projects/{name}")]
    [ApiController]
    public class ConfigurationsController : ControllerBase
    {
        private readonly IConfigurations _configurations;

        public ConfigurationsController(IConfigurations configurations)
        {
            _configurations = configurations;
        }

        [HttpGet("configs/{env}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<object>> GetConfig(string name, string env, [FromHeader] string apiKey)
        {
            var config = await _configurations.GetItem(name, env, apiKey);
            return Ok(JsObject.Create(config));
        }
    }
}
