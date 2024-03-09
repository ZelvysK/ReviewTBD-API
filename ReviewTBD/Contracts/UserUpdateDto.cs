using ReviewTBDAPI.Models;

namespace ReviewTBDAPI.Contracts;

public class UserUpdateDto
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}

public class AdminUpdateDto
{
    // public string? Username { get; set; }
    // public string? Email { get; set; }
    // public string? PhoneNumber { get; set; }
    public Role Role { get; set; }
}

public class UpdatePasswordDto
{
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
    public string? PasswordToken { get; set; }
}