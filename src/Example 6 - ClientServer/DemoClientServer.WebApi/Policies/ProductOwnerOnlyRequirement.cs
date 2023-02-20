using DemoClientServer.WebApi.Repository;
using Microsoft.AspNetCore.Authorization;

namespace DemoClientServer.WebApi.Policies;

public class ProductOwnerOnlyRequirement : IAuthorizationRequirement
{
}

public class ProductOwnerOnlyHandler : AuthorizationHandler<ProductOwnerOnlyRequirement, Product>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ProductOwnerOnlyRequirement requirement, Product resource)
    {
        if (context.User.Identity?.Name == resource.OwnerId.ToString())
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
