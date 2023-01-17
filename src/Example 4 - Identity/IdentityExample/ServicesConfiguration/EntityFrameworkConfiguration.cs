using IdentityExample.Data;
using Microsoft.EntityFrameworkCore;

namespace IdentityExample.ServicesConfiguration
{
    public static class EntityFrameworkConfiguration
    {
        public static void RegisterEntityFramework(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("Memory");
            });
        }
    }
}
