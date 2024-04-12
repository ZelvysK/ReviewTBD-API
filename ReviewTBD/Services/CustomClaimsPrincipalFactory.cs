using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using ReviewTBDAPI.Models;

namespace ReviewTBDAPI.Services;

public static class ClaimsExtensions
{
    public const string Role = "Role";
}

public class ExtendedClaimsTransformer(UserManager<ApplicationUser> userManager)
    : IClaimsTransformation
{
    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        var identity = principal.Identity;

        if (identity is null)
        {
            return principal;
        }

        if (identity.IsAuthenticated)
        {
            var user = await userManager.GetUserAsync(principal);

            if (user is not null && identity is ClaimsIdentity claimsIdentity)
            {
                claimsIdentity.AddClaim(new Claim(ClaimsExtensions.Role, user.Role.ToString()));
            }
        }

        return principal;
    }
}
