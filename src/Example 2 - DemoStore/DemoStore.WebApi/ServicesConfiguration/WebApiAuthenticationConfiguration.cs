using DemoStore.Identity.Configurations;
using DemoStore.Identity.Constants;
using DemoStore.Identity.PolicyRequirements;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DemoStore.WebApi.ServicesConfiguration
{
    public static class WebApiAuthenticationConfiguration
    {
        public static void RegisterWebApiAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetSection(nameof(JwtOptions))["SecurityKey"]));

            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = configuration.GetSection("JwtOptions:Issuer").Value,

                ValidateAudience = true,
                ValidAudience = configuration.GetSection("JwtOptions:Audience").Value,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = securityKey,

                RequireExpirationTime = true,
                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = tokenValidationParameters;
            });
        }

        public static void RegisterWebApiAuthorizationPolicies(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationHandler, BusinessHoursHandler>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.BusinessHours, policy =>
                {
                    policy.Requirements.Add(new BusinessHoursRequirement());
                });
            });
        }
    }
}
