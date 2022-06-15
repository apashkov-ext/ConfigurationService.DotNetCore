using ConfigurationManagementSystem.Application.Dto;
using ConfigurationManagementSystem.Application.Stories.SignInStory;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ConfigurationManagementSystem.Api.Controllers;

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
    public async Task<ActionResult<TokenDto>> SignIn(SignInDto request)
    {
        return Ok(await _signInStory.ExecuteAsync(request.Username, request.Password));
    }
}
