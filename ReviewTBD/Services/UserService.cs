using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReviewTBDAPI.Contracts;
using ReviewTBDAPI.Contracts.Queries;
using ReviewTBDAPI.Utilities;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace ReviewTBDAPI.Services;

public interface IUserService
{
    Task<IdentityUser?> GetUserByIdAsync(string id);
    Task<IdentityResult> RegisterUserAsync(RegisterDto input);
    Task<SignInResult> LoginUserAsync(LoginDto input);
    Task<IdentityUser?> GetUserByUsernameAsync(string username);
    Task<IdentityUser?> GetUserByEmailAsync(string email);
    Task<PaginatedResult<UserDto>> GetAllUsersAsync(UserQuery filters);
    Task<IdentityResult> ChangeEmailAsync(string id, UserEmailDto input);
    Task<IdentityResult> ChangePhoneNumberAsync(string id, UserPhoneDto input);
    Task<IdentityResult> ResetPasswordAsync(string id, UserPasswordDto input);
    Task<IdentityResult> ChangePasswordAsync(string id, UserPasswordDto input);
    Task<IdentityUser?> UpdateUserAsync(string id, EdidUserDto input);
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

        var entry = await userManager.FindByIdAsync(id);

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

    public async Task<PaginatedResult<UserDto>> GetAllUsersAsync(UserQuery filters)
    {
        logger.LogInformation("Get all users with filters: {filters}", filters);

        var query = context.Users.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(filters.Term))
            query = query.Where(u => u.UserName.Contains(filters.Term) || u.Email.Contains(filters.Term));

        var entries = await query
            .Select(s => new UserDto
            {
                Username = s.UserName,
                Email = s.Email
            })
            .AddPagination(filters.Offset, filters.Limit)
            .ToArrayAsync();

        var totalCount = await query.CountAsync();

        return new PaginatedResult<UserDto>
        {
            Limit = filters.Limit,
            Offset = filters.Offset,
            Result = entries,
            Total = totalCount
        };
    }

    public async Task<IdentityResult> ChangeEmailAsync(string id, UserEmailDto input)
    {
        var user = await userManager.FindByIdAsync(id);

        input.EmailToken = await userManager.GenerateChangeEmailTokenAsync(user, input.Email);
        var updated = await userManager.ChangeEmailAsync(user, input.Email, input.EmailToken);
        if (updated.Succeeded)
            logger.LogInformation("Email changed successfully!");
        else
            logger.LogInformation("Failed to change email...");
        return updated;

        return updated;
    }

    public async Task<IdentityResult> ChangePhoneNumberAsync(string id, UserPhoneDto input)
    {
        var user = await userManager.FindByIdAsync(id);

        var hasPhoneNumber = await userManager.GetPhoneNumberAsync(user);
        if (hasPhoneNumber is not null)
        {
            input.PhoneToken = await userManager.GenerateChangePhoneNumberTokenAsync(user, input.PhoneNumber);
            var updated = await userManager.ChangePhoneNumberAsync(user, input.PhoneNumber, input.PhoneToken);
            if (updated.Succeeded)
                logger.LogInformation("Phone number changed successfully!");
            else
                logger.LogInformation("Failed to change phone number...");
            return updated;
        }

        var added = await userManager.SetPhoneNumberAsync(user, input.PhoneNumber);
        if (added.Succeeded)
            logger.LogInformation("Phone number added!");
        else
            logger.LogInformation("Failed to add phone number...");
        return added;
    }

    public async Task<IdentityResult> ResetPasswordAsync(string id, UserPasswordDto input)
    {
        var user = await userManager.FindByIdAsync(id);

        //Get from email or phone
        input.PasswordToken = await userManager.GeneratePasswordResetTokenAsync(user);
        var updated = await userManager.ResetPasswordAsync(user, input.PasswordToken, input.NewPassword);

        return updated;
    }

    public async Task<IdentityResult> ChangePasswordAsync(string id, UserPasswordDto input)
    {
        var user = await userManager.FindByIdAsync(id);

        var hasPassword = await userManager.HasPasswordAsync(user);
        if (!hasPassword)
        {
            var added = await userManager.AddPasswordAsync(user, input.NewPassword);
            if (added.Succeeded)
                logger.LogInformation("Password added successfully!");
            else
                logger.LogInformation("Failed to add password...");
            return added;
        }

        var updated = await userManager.ChangePasswordAsync(user, input.CurrentPassword, input.NewPassword);
        if (updated.Succeeded)
            logger.LogInformation("Password changed successfully!");
        else
            logger.LogInformation("Failed to change password...");
        return updated;
    }

    public async Task<IdentityUser?> UpdateUserAsync(string id, EdidUserDto input)
    {
        var user = await userManager.FindByIdAsync(id);

        if (input.Username is not null && user.UserName != input.Username)
        {
            var updated = await userManager.SetUserNameAsync(user, input.Username);
            if (!updated.Succeeded) return null;
        }
        else
        {
            logger.LogInformation("Username has not been changed");
        }

        if (input.Email is not null && user.Email != input.Email)
        {
            //Should be gotten from Email
            input.EmailToken = await userManager.GenerateChangeEmailTokenAsync(user, input.Email);
            var updated = await userManager.ChangeEmailAsync(user, input.Email, input.EmailToken);
            if (!updated.Succeeded) return null;
        }
        else
        {
            logger.LogInformation("Email has not been changed");
        }

        if (input.NewPassword is not null)
        {
            var result = await userManager.ChangePasswordAsync(user, input.CurrentPassword, input.NewPassword);
            if (!result.Succeeded) return null;
        }
        else
        {
            logger.LogInformation("Password has not been changed");
        }

        if (input.PhoneNumber is not null && user.PhoneNumber != input.PhoneNumber)
        {
            //Should be gotten from Phone Number
            input.PhoneToken = await userManager.GenerateChangePhoneNumberTokenAsync(user, input.PhoneNumber);
            var updated = await userManager.ChangePhoneNumberAsync(user, input.PhoneNumber, input.PhoneToken);
            if (!updated.Succeeded) return null;
        }
        else
        {
            logger.LogInformation("Phone number has not been changed");
        }

        return user;
    }
}