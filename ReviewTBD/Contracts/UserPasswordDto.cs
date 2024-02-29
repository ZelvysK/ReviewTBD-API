namespace ReviewTBDAPI.Contracts;

public class UserPasswordDto
{
    public string? CurrentPassword { get; set; }
    public string? NewPassword { get; set; }
    public string? PasswordToken { get; set; }
}