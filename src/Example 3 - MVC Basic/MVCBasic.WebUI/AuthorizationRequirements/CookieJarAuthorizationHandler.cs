using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace MVCBasic.WebUI.AuthorizationRequirements;

public class CookieJarAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, CookieJar>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        OperationAuthorizationRequirement requirement, 
        CookieJar cookieJar)
    {
        if (requirement.Name == CookieJarOperationTypes.Look)
        {
            if (context.User.Identity.IsAuthenticated)
                context.Succeed(requirement);
        }
        else if (requirement.Name == CookieJarOperationTypes.ComeNear)
        {
            if (context.User.HasClaim("Friend", "Good"))
                context.Succeed(requirement);
        }


        return Task.CompletedTask;
    }
}

// This could be an entity for example...
public class CookieJar
{
    public string Name { get; set; }
}

public static class CookieJarAuthorizationOperations
{
    public static OperationAuthorizationRequirement Open = new OperationAuthorizationRequirement()
    {
        Name = CookieJarOperationTypes.Open
    };
}

public static class CookieJarOperationTypes
{
    public static string Look = "Look";
    public static string ComeNear = "ComeNear";
    public static string Open = "Open";
    public static string Take = "Take";
}
