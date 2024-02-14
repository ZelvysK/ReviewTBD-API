
using System.Text.Json.Serialization;

namespace ReviewTBDAPI.Contracts;

public class UserDto {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    [JsonIgnore] public string Password { get; set; }
}
