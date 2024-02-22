using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReviewTBDAPI.Contracts;

namespace ReviewTBDAPI.Services;

public interface IUserService
{
    Task<IdentityUser?> GetUserByIdAsync(string id);
    Task<IdentityResult> RegisterUserAsync(RegisterDto input);
    Task<SignInResult> LoginUserAsync(LoginDto input);
}

public class UserService(ReviewContext context, UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager, ILogger<UserService> logger) : IUserService
{
    public async Task<IdentityUser?> GetUserByIdAsync(string id)
    {
        logger.LogInformation("Get user by id: {id}", id);

        var entry = await context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id.ToString() == id);
        
        return entry;
    }

    public async Task<IdentityResult> RegisterUserAsync(RegisterDto input)
    {
        logger.LogInformation("Register user with input: {input}", input);

        var username = input.Username.ToLower();

        var user = new IdentityUser { UserName = username, Email = input.Email };

        var result = await userManager.CreateAsync(user, input.Password);

        return result;
    }

    public async Task<SignInResult> LoginUserAsync(LoginDto input)
    {
        var normalizedUsername = input.Username.ToUpper();

        var result = await signInManager.PasswordSignInAsync(normalizedUsername, input.Password, isPersistent: false,
            lockoutOnFailure: false);

        return result;
    }

}