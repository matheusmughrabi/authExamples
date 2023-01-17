using NETCore.MailKit.Infrastructure.Internal;

namespace IdentityExample.Configurations
{
    public static class AppSettingsConfigurations
    {
        public static MailKitOptions GetMailKitOptions(this IConfiguration configuration)
        {
            return configuration.GetSection(nameof(MailKitOptions)).Get<MailKitOptions>();
        }
    }
}
