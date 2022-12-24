using DemoStore.Identity.Configurations;
using DemoStore.Identity.Data;
using DemoStore.Identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DemoStore.WebApi.ServicesConfiguration
{
    public static class IdentityServiceConfiguration
    {
        public static void RegisterIdentityService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityDataContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("IdentityDatabase")));

            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<IdentityDataContext>()
                .AddDefaultTokenProviders();

            var jwtOptionsAppSettings = configuration.GetSection(nameof(JwtOptions));
            services.AddScoped(c => configuration.BuildJwtOptions());

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 10;
            });

            services.AddScoped<IIdentityService, IdentityService>();
        }

        private static JwtOptions BuildJwtOptions(this IConfiguration configuration)
        {
            var section = configuration.GetSection(nameof(JwtOptions));
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(section["SecurityKey"]));

            return new JwtOptions()
            {
                Audience = section["Audience"],
                ExpirationInSeconds = int.Parse(section["ExpirationInSeconds"]),
                Issuer = section["Issuer"],
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            };
        }
    }
}
