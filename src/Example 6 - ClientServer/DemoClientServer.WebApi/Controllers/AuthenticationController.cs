using DemoClientServer.WebApi.Constants;
using DemoClientServer.WebApi.Repository;
using DemoClientServer.WebApi.Requests;
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

    [HttpPost("Authentication/GetAccessToken")]
    public async Task<IActionResult> GetAccessToken([FromBody] GetAccessTokenRequest request)
    {
        var user = _userRepository.GetUserWithClaims(request.Username, request.Password);

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

        var response = new GetAccessTokenResponse() { Token = tokenString };

        return Ok(response);
    }
}
