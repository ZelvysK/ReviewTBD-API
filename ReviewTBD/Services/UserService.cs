using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReviewTBDAPI.Contracts;

namespace ReviewTBDAPI.Services;

public interface IUserService
{
    Task<IdentityUser?> GetUserByIdAsync(string id);
    Task<IdentityResult> RegisterUserAsync(RegisterDto input);
    Task<SignInResult> LoginUserAsync(LoginDto input);
    Task<IdentityUser?> GetUserByUsernameAsync(string username);
    Task<IdentityUser?> GetUserByEmailAsync(string email);
}

public class UserService(
    ReviewContext context,
    UserManager<IdentityUser> userManager,
    SignInManager<IdentityUser> signInManager,
    ILogger<UserService> logger) : IUserService
{
    public async Task<IdentityUser?> GetUserByIdAsync(string id)
    {
        logger.LogInformation("Get user by id: {id}", id);

        var entry = await context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);

        return entry;
    }

    public async Task<IdentityResult> RegisterUserAsync(RegisterDto input)
    {
        logger.LogInformation("Register user with provided input");

        var username = input.Username.ToLower();

        var user = new IdentityUser { UserName = username, Email = input.Email };

        var result = await userManager.CreateAsync(user, input.Password);

        return result;
    }

    public async Task<SignInResult> LoginUserAsync(LoginDto input)
    {
        var normalizedUsername = input.Username.ToUpper();

        if (normalizedUsername.Contains('@'))
        {
            var user = await GetUserByEmailAsync(normalizedUsername);
            var emailResult = await signInManager.PasswordSignInAsync(user.NormalizedUserName, input.Password, false,
                false);
            
            return emailResult;
        }
        var usernameResult = await signInManager.PasswordSignInAsync(normalizedUsername, input.Password, false,
            false);
        
        return usernameResult;
    }

    public async Task<IdentityUser?> GetUserByUsernameAsync(string username)
    {
        var entry = await context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.UserName == username);

        return entry;
    }
    
    public async Task<IdentityUser?> GetUserByEmailAsync(string email)
    {
        var entry = await context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email);

        return entry;
    }
}