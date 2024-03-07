using System.Security.Claims;

namespace ReviewTBDAPI.Utilities;

public static class ClaimsPrincipalExtensions
{
    public static Guid? GetId(this ClaimsPrincipal principal)
    {
        var idClaim = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        return idClaim is not null
            ? new Guid(idClaim)
            : null;
    }
}