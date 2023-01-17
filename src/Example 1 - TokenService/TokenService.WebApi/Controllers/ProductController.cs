using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TokenService.WebApi.Controllers;

public class ProductController : ControllerBase
{
    [HttpGet("get")]
    public async Task<ActionResult> Get()
    {
        var user = User.Identity?.Name;
        return Ok();
    }

    [HttpPost("create")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Create()
    {
        var user = User.Identity?.Name;
        return Ok();
    }

    [HttpGet("buy")]
    [Authorize(Roles = "Client")]
    public async Task<ActionResult> Buy()
    {
        var user = User.Identity?.Name;
        return Ok();
    }
}