using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using MVCBasic.WebUI.AuthorizationRequirements;

namespace MVCBasic.WebUI.Controllers;

public class OperationsController : Controller
{
    private readonly IAuthorizationService _authorizationService;

    public OperationsController(IAuthorizationService authorizationService)
    {
        _authorizationService = authorizationService;
    }

    public async Task<IActionResult> Open()
    {
        // Get the cookie from db for example
        var cookieJar = new CookieJar();

        var requirement = new OperationAuthorizationRequirement()
        {
            Name = CookieJarOperationTypes.Open
        };

        //var authResult = await _authorizationService.AuthorizeAsync(User, cookieJar, requirement);
        var authResult = await _authorizationService.AuthorizeAsync(User, cookieJar, CookieJarAuthorizationOperations.Open);
        if (!authResult.Succeeded)
            return Unauthorized();

        return View();
    }
}
