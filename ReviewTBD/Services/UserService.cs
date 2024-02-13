using Microsoft.EntityFrameworkCore;
using ReviewTBDAPI.Contracts;
using ReviewTBDAPI.Models;

namespace ReviewTBDAPI.Services;

public interface IUserService
{
    Task<Guid> CreateUserAsync(UserDto userDto);
    Task<UserDto?> GetUserByEmailAsync(string email);
    Task<UserDto?> GetUserByIdAsync(Guid id);
}

public class UserService(ReviewContext context, ILogger<UserService> logger) : IUserService
{
    public async Task<Guid> CreateUserAsync(UserDto userDto) {
        var user = User.FromDto(userDto);

        context.Users.Add(user);

        await context.SaveChangesAsync();

        return user.Id;
    }

    public async Task<UserDto?> GetUserByIdAsync(Guid id) {
        logger.LogInformation("Get user by id: {id}", id);

        var entry = await context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);

        var result = entry?.ToDto();
        
        return result;
    }
    
    public async Task<UserDto?> GetUserByEmailAsync(string email) {
        logger.LogInformation("Get user by email: {email}", email);

        var entry = await context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email);

        var result = entry?.ToDto();

        return result;
    }

}
