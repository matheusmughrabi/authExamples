using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MVCBasic.WebUI.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        var user = HttpContext.User;
        return View();
    }

    [Authorize]
    public IActionResult Secret()
    {
        return View();
    }

    [Authorize(Policy = "Policy_DateOfBirth")]
    public IActionResult SecretWithPolicy()
    {
        return View("Secret");
    }

    public IActionResult Authenticate()
    {
        var grandmaClaims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, "Bob"),
            new Claim(ClaimTypes.Email, "Bob@gmail.com"),
            new Claim(ClaimTypes.DateOfBirth, "01/01/1990"),
            new Claim("Grandma.Says", "Very nice boy")
        };

        var licenseClaims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, "Bob K Foo"),
            new Claim("DrivingLicense", "A+")
        };

        var grandmaIdentity = new ClaimsIdentity(grandmaClaims, "Grandma Identity");
        var licenseIdentity = new ClaimsIdentity(licenseClaims, "Government");

        var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity, licenseIdentity });

        HttpContext.SignInAsync(userPrincipal);

        return RedirectToAction("Index");
    }
}
