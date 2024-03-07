using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ReviewTBDAPI.Services;

namespace ReviewTBDAPI.Filters;

public class AdminAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (context.HttpContext.User.Identity is { IsAuthenticated: false })
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        if (!context.HttpContext.User.Claims.Any(c => c is { Type: ClaimsExtensions.Role, Value: "Admin" }))
        {
            context.Result = new ForbidResult();
        }
    }
}