using ConfigurationManagementSystem.Api.Dto;
using ConfigurationManagementSystem.Application.Stories.SignInStory;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInStory _signInStory;

        public AuthController(SignInStory signInStory)
        {
            _signInStory = signInStory;
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInDto request)
        {
            var token = await _signInStory.ExecuteAsync(request.Username, request.Password);
            return Ok(new { token });
        }
    }
}
