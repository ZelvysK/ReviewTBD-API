using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ReviewTBDAPI.Services;

public interface IUserService
{
    Task<IdentityUser?> GetUserByIdAsync(string id);
}

public class UserService(ReviewContext context, ILogger<UserService> logger) : IUserService
{
    public async Task<IdentityUser?> GetUserByIdAsync(string id)
    {
        logger.LogInformation("Get user by id: {id}", id);

        var entry = await context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id.ToString() == id);
        
        return entry;
    }

}