using Microsoft.AspNetCore.Authorization;

namespace DemoStore.Identity.PolicyRequirements
{
    public class BusinessHoursRequirement : IAuthorizationRequirement
    {
    }

    public class BusinessHoursHandler : AuthorizationHandler<BusinessHoursRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, BusinessHoursRequirement requirement)
        {
            var currentTime = TimeOnly.FromDateTime(DateTime.Now);
            if (currentTime.Hour >= 8 && currentTime.Hour <= 18)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
