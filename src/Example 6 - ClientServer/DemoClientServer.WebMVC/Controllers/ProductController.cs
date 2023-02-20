using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoClientServer.WebMVC.Controllers;

public class ProductController : Controller
{
    [Authorize]
    public async Task<IActionResult> Index()
    {
        return View();
    }
}
