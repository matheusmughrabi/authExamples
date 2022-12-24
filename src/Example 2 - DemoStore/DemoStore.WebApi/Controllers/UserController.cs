using DemoStore.Identity.Services;
using DemoStore.Identity.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoStore.WebApi.Controllers;

public class UserController : ControllerBase
{
    private readonly IIdentityService _identityService;

    public UserController(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _identityService.RegisterUser(request);
        if (result.Success)
        {
            return Ok(result);
        }
        else if (result.Errors?.Count > 0)
        {
            return BadRequest(result);
        }

        return StatusCode(StatusCodes.Status500InternalServerError);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _identityService.LoginUser(request);
        if (result.Success)
        {
            return Ok(result);
        }
        else if (result.Errors?.Count > 0)
        {
            return BadRequest(result);
        }

        return Unauthorized(result);
    }
}
