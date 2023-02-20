using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using OAuthExample.WebApi.Constants;

namespace OAuthExample.WebApi.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [Authorize]
    public IActionResult Secret()
    {
        return View();
    }

    public IActionResult Authenticate()
    {
        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Name, ""),
            new Claim("Age", "27")
        };

        var secretEncoded = Encoding.UTF8.GetBytes(TokenConstants.Secret);
        var securityKey = new SymmetricSecurityKey(secretEncoded);
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            TokenConstants.Issuer,
            TokenConstants.Audience,
            claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials
            );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return Ok(new { acessToken = tokenString });
    }
}
