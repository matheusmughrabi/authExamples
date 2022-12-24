using TokenService.WebApi.Services;

namespace TokenService.WebApi.ServicesConfiguration
{
    public static class TokenGeneratorServiceConfiguration
    {
        public static void RegisterTokenGenerator(this IServiceCollection services, IConfiguration configuration)
        {
            var key = configuration["Secret"];
            services.AddTransient<TokenGeneratorService>(_ => new TokenGeneratorService(key));
        }
    }
}
