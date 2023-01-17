using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NETCore.MailKit.Core;

namespace IdentityExample.Controllers;

public class HomeController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signManager;
    private readonly IEmailService _emailService;

    public HomeController(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signManager, 
        IEmailService emailService)
    {
        _userManager = userManager;
        _signManager = signManager;
        _emailService = emailService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [Authorize]
    public IActionResult Secret()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string username, string password)
    {
        var identityUser = await _userManager.FindByNameAsync(username);

        if (identityUser != null)
        {
            var signInResult = await _signManager.PasswordSignInAsync(identityUser, password, false, true);

            if (signInResult.Succeeded)
                return RedirectToAction("Index");
        }


        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(string username, string password)
    {
        var identityUser = new IdentityUser()
        {
            UserName = username
        };

        var createUserResult = await _userManager.CreateAsync(identityUser, password);

        if (createUserResult.Succeeded)
        {
            // Generate email token
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(identityUser);
            var link = Url.Action(nameof(EmailVerified), "Home", new { userId = identityUser.Id, code = code }, Request.Scheme, Request.Host.ToString());

            await _emailService.SendAsync("test@gmail.com", "email verify,", $"<a href=\"{link}\">Verify email</a>", true);

            return RedirectToAction("MustVerifyEmail");
        }

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> MustVerifyEmail()
    {
        return View();
    }

    public async Task<IActionResult> EmailVerified(string userId, string code)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return BadRequest();

        var result = await _userManager.ConfirmEmailAsync(user, code);
        if (result.Succeeded) return View();

        return BadRequest();
    }

    public async Task<IActionResult> LogOut()
    {
        await _signManager.SignOutAsync();
        return RedirectToAction("Index");
    }
}
