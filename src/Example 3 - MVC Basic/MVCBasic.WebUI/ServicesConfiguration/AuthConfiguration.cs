using Microsoft.AspNetCore.Authorization;
using MVCBasic.WebUI.AuthorizationRequirements;
using System.Security.Claims;

namespace MVCBasic.WebUI.ServicesConfiguration;

public static class AuthConfiguration
{
    public static void RegisterCookies(this IServiceCollection services)
    {
        services.AddAuthentication("CookieAuth")
            .AddCookie("CookieAuth", config =>
            {
                config.Cookie.Name = "Grandmas.Cookie";
                config.LoginPath = "/Home/Authenticate";
            });
    }

    /// <summary>
    /// This is an example of the default policy of AspNet Core 
    /// </summary>
    /// <param name="services"></param>
    public static void RegisterDefaultPolicy(this IServiceCollection services)
    {
        services.AddAuthorization(config =>
        {
            // This is how the default policy is built
            var defaultAuthBuilder = new AuthorizationPolicyBuilder();
            var defaultAuthPolicy = defaultAuthBuilder
                .RequireAuthenticatedUser()
                .Build();

            config.DefaultPolicy = defaultAuthPolicy;
        });
    }

    public static void RegisterDateOfBirthPolicy(this IServiceCollection services)
    {
        services.AddAuthorization(config =>
        {
            config.AddPolicy("Policy_DateOfBirth", policyBuilder =>
            {
                policyBuilder.AddRequirements(new CustomRequireClaim(ClaimTypes.DateOfBirth));
            });
        });

        services.AddScoped<IAuthorizationHandler, CustomRequireClaimHandler>();
        services.AddScoped<IAuthorizationHandler, CookieJarAuthorizationHandler>();
    }
}
