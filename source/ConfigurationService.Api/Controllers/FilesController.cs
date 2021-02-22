using System;
using System.IO;
using System.Threading.Tasks;
using ConfigurationService.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConfigurationService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IConfigurations _configurations;

        public FilesController(IConfigurations configurations)
        {
            _configurations = configurations;
        }

        [HttpPost("api/projects/{projectId}/configs/{env}")]
        [DisableRequestSizeLimit]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Import(Guid projectId, string env)
        {
            var file = Request.Form.Files[0];
            if (file.Length <= 0)
            {
                return BadRequest();
            }

            await using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            await _configurations.Import(projectId, env, stream.ToArray());

            return Ok();
        }
    }
}
