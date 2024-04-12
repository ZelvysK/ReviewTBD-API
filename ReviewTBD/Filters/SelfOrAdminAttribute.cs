using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ReviewTBDAPI.Services;
using ReviewTBDAPI.Utilities;

namespace ReviewTBDAPI.Filters;

public class SelfOrAdminAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (context.HttpContext.User.Identity is { IsAuthenticated: false })
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        if (
            context.HttpContext.User.Claims.Any(c =>
                c is { Type: ClaimsExtensions.Role, Value: "Admin" }
            )
        )
        {
            return;
        }

        if (
            context.HttpContext.Request.RouteValues.TryGetValue("id", out var idParameter)
            && idParameter is string userId
        )
        {
            if (context.HttpContext.User.GetId().ToString() == userId)
            {
                return;
            }
        }

        context.Result = new ForbidResult();
    }
}
