using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;

namespace IdentityExample.ServicesConfiguration
{
    public static class EmailConfigurations
    {
        public static void RegisterMailKit(this IServiceCollection services, MailKitOptions mailKitOptions)
        {
            services.AddMailKit(config =>
            {
                config.UseMailKit(mailKitOptions);
            });
        }
    }
}
