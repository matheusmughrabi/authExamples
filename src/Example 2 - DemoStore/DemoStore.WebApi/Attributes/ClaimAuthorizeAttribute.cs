﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace DemoStore.WebApi.Attributes
{
    public class ClaimAuthorizeAttribute : TypeFilterAttribute
    {
        public ClaimAuthorizeAttribute(string claimType, string claimValue) : base(typeof(ClaimRequirementFilter))
            => Arguments = new object[] { new Claim(claimType, claimValue) };
    }

    public class ClaimRequirementFilter : IAuthorizationFilter
    {
        private readonly Claim _claim;

        public ClaimRequirementFilter(Claim claim)
        {
            _claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (user == null || !user.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if(!user.HasClaim(_claim.Type, _claim.Value))
                context.Result = new ForbidResult();
        }
    }
}
