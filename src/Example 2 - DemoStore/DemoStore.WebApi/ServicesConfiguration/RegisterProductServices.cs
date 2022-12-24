using DemoStore.WebApi.Models.Product;

namespace DemoStore.WebApi.ServicesConfiguration
{
    public static class RegisterProductServices
    {
        public static void RegisterProductRepositories(this IServiceCollection services)
        {
            services.AddScoped<ProductRepositoryMock>();
        }
    }
}
