using DemoClientServer.WebMVC.ApiClients.Authentication;
using DemoClientServer.WebMVC.Requests;
using Microsoft.AspNetCore.Mvc;

namespace DemoClientServer.WebMVC.Controllers;

public class AuthenticationController : Controller
{
    private readonly IAuthenticationApiClient _authenticationApiClient;

    public AuthenticationController(IAuthenticationApiClient authenticationApiClient)
    {
        _authenticationApiClient = authenticationApiClient;
    }

    [HttpGet]
    public async Task<IActionResult> Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string username, string password)
    {
        var response = await _authenticationApiClient.GetAccessTokenAsync(new GetAccessTokenRequest() { Username = username, Password = password });    
        Response.Cookies.Append("X-Access-Token", response.Token, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
        return View();
    }
}
