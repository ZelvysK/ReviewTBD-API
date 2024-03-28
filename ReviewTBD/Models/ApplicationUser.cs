using Microsoft.AspNetCore.Identity;
using ReviewTBDAPI.Contracts;

namespace ReviewTBDAPI.Models;

public enum Role
{
    Admin,
    User
}

public class ApplicationUser : IdentityUser<Guid>
{
    public Role Role { get; set; }
    public bool FirstTimeLogin { get; set; }

    public void Update(UserUpdateDto updated)
    {
        UserName = updated.Username;
        NormalizedUserName = updated.Username.ToUpper();
        Email = updated.Email;
        NormalizedEmail = updated.Email.ToUpper();
        PhoneNumber = updated.PhoneNumber;
        FirstTimeLogin = false;
    }
}