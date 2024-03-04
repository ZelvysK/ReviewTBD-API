namespace ReviewTBDAPI.Contracts;

public class EditUserDto
{
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? EmailToken { get; set; }
    public string? CurrentPassword { get; set; }
    public string? NewPassword { get; set; }
    public string? PasswordToken { get; set; }
    public string? PhoneNumber { get; set; }
    public string? PhoneToken { get; set; }
}