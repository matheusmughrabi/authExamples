using Microsoft.AspNetCore.Authorization;

namespace MVCBasic.WebUI.AuthorizationRequirements;

public class CustomRequireClaim : IAuthorizationRequirement
{
    public CustomRequireClaim(string claimType)
    {
        ClaimType = claimType;
    }

    public string ClaimType { get; set; }
}

public class CustomRequireClaimHandler : AuthorizationHandler<CustomRequireClaim>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomRequireClaim requirement)
    {
        var hasClaim = context.User.Claims.Any(c => c.Type == requirement.ClaimType);
        if (hasClaim)
            context.Succeed(requirement);

        return Task.CompletedTask;  
    }
}
