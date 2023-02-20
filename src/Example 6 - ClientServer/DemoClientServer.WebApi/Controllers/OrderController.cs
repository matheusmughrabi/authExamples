using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoClientServer.WebApi.Controllers;

public class OrderController : ControllerBase
{
    [HttpPost("Order/Buy")]
    [Authorize]
    public async Task<IActionResult> Buy()
    {
        return Ok("Product bought");
    }
}
