using Microsoft.AspNetCore.Identity;

namespace ReviewTBDAPI.Models;

public enum Role
{
    Admin,
    User
}

public class ApplicationUser : IdentityUser<Guid>
{
    public Role Role { get; set; }
}
