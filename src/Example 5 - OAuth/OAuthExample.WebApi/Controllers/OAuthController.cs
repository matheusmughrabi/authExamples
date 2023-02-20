using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OAuthExample.WebApi.Constants;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OAuthExample.WebApi.Controllers;

public class OAuthController : Controller
{
    [HttpGet]
    public IActionResult Authorize(
        string response_type, // authorization flow type
        string client_id,
        string redirect_uri,
        string scope, // what info I want
        string state) // random string generate to confirm that we are going back to the same client
    {
        var query = new QueryBuilder();
        query.Add("redirect_uri", redirect_uri);
        query.Add("state", state);

        return View(model: query.ToString());
    }

    [HttpPost]
    public IActionResult Authorize(
        string username,
        string password,
        string redirect_uri,
        string state)
    {
        const string code = "BABABABABABA";

        var query = new QueryBuilder();
        query.Add("code", code);
        query.Add("state", state);

        return Redirect($"{redirect_uri}{query.ToString()}");
    }

    public async Task<IActionResult> Token(
        string grant_type,
        string code,
        string redirect_uri,
        string client_id)
    {
        // Validate the code

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

        var access_token = new JwtSecurityTokenHandler().WriteToken(token);

        var response = new
        {
            access_token,
            token_type = "Bearer",
            raw_claim = "oauthTutorial"
        };

        var responseJson = System.Text.Json.JsonSerializer.Serialize(response);
        var responseBytes = Encoding.UTF8.GetBytes(responseJson);
        //await Response.Body.WriteAsync(responseBytes, 0, responseBytes.Length);
        //await HttpContext.Response.WriteAsync(responseJson);

        return Redirect(redirect_uri);
    }
}
