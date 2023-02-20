using DemoClientServer.WebApi.Constants;
using DemoClientServer.WebApi.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DemoClientServer.WebApi.Controllers;

public class AuthenticationController : ControllerBase
{
    private readonly UserRepository _userRepository;

    public AuthenticationController(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet("Authentication/GetAccessToken")]
    public async Task<IActionResult> GetAccessToken(string username, string password)
    {
        var user = _userRepository.GetUserWithClaims(username, password);

        var claims = user.UserClaims.Select(claim => new Claim(claim.ClaimType, claim.ClaimValue)).ToList();
        claims.Add(new Claim(ClaimTypes.Name, user.Id.ToString()));

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

        return Ok(tokenString);
    }
}
