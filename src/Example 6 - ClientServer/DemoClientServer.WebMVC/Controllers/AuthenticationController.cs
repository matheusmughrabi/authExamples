using Microsoft.AspNetCore.Mvc;

namespace DemoClientServer.WebMVC.Controllers;

public class AuthenticationController : Controller
{
    [HttpGet]
    public async Task<IActionResult> Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string username, string password)
    {
        // Call Web API to get token
        var httpClient = new HttpClient();
        var result = await httpClient.GetAsync($"https://localhost:7098/authentication/GetAccessToken?username={username}&password={password}");
        
        var token = await result.Content.ReadAsStringAsync();
        Response.Cookies.Append("X-Access-Token", token, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });

        return View();
    }
}
