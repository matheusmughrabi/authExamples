using DemoStore.Identity.Configurations;
using DemoStore.Identity.Services.Models;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DemoStore.Identity.Services;

public class IdentityService : IIdentityService
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly JwtOptions _jwtOptions;

    public IdentityService(
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager,
        JwtOptions jwtOptions)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _jwtOptions = jwtOptions;
    }

    public async Task<RegisterUserResponse> RegisterUser(RegisterUserRequest request)
    {
        var identityUser = new IdentityUser()
        {
            UserName = request.Email,
            Email = request.Email,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(identityUser, request.Password);
        if (result.Succeeded)
            await _userManager.SetLockoutEnabledAsync(identityUser, false);

        var registerUserResponse = new RegisterUserResponse()
        {
            Success = result.Succeeded
        };

        if (!result.Succeeded && result.Errors.Count() > 0)
            registerUserResponse.AddErrors(result.Errors.Select(c => c.Description).ToList());

        return registerUserResponse;
    }

    public async Task<LoginUserResponse> LoginUser(LoginUserRequest request)
    {
        var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, false, true);

        if (result.Succeeded)
        {
            return new LoginUserResponse()
            {
                Success = true,
                Token = await GenerateToken(request.Email)
            };
        }

        var loginUserResponse = new LoginUserResponse() { Success = result.Succeeded };

        if (!result.Succeeded)
        {
            if (result.IsLockedOut)
                loginUserResponse.AddError("This account is locked");
            else if (result.IsNotAllowed)
                loginUserResponse.AddError("This account can1t perform login");
            else if (result.RequiresTwoFactor)
                loginUserResponse.AddError("You must confirm the login in your second factor authentication device");
            else
                loginUserResponse.AddError("Incorret email or password");
        }

        return loginUserResponse;
    }

    private async Task<string> GenerateToken(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        var userClaims = await GetUserClaims(user);

        var expirationDate = DateTime.UtcNow.AddSeconds(_jwtOptions.ExpirationInSeconds);

        var jwt = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            notBefore: DateTime.Now,
            expires: expirationDate,
            signingCredentials: _jwtOptions.SigningCredentials,
            claims: userClaims);

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    private async Task<IList<Claim>> GetUserClaims(IdentityUser user)
    {
        var claims = await _userManager.GetClaimsAsync(user);

        claims.Add(new Claim("Id", user.Id));
        claims.Add(new Claim(ClaimTypes.Name, user.Email));

        var roles = await _userManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        return claims;
    }
}
