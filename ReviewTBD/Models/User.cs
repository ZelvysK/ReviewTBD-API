using ReviewTBDAPI.Contracts;

namespace ReviewTBDAPI.Models;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public UserDto ToDto() => new()
    {
        Id = Id,
        Name = Name,
        Email = Email,
        Password = Password
    };

    public static User FromDto(UserDto dto) => new()
    {
        Name = dto.Name,
        Email = dto.Email,
        Password = dto.Password
    };

    public void Update(UserDto update) {
        Name = update.Name;
        Email = update.Email;
        Password = update.Password;
    }
}
