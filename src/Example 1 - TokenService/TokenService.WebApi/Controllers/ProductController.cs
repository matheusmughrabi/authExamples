using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TokenService.WebApi.Controllers;

public class ProductController : ControllerBase
{
    [HttpGet("get")]
    public async Task<ActionResult> Get()
    {
        return Ok();
    }

    [HttpPost("create")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Create()
    {
        return Ok();
    }

    [HttpGet("buy")]
    [Authorize(Roles = "Client")]
    public async Task<ActionResult> Buy()
    {
        return Ok();
    }
}