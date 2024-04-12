using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReviewTBDAPI.Contracts;
using ReviewTBDAPI.Contracts.Queries;
using ReviewTBDAPI.Models;
using ReviewTBDAPI.Utilities;

namespace ReviewTBDAPI.Services;

public interface IUserService
{
    Task<UserDto?> GetUserByIdAsync(Guid id);
    Task<PaginatedResult<UserDto>> GetAllUsersAsync(UserQuery filters);
    Task<MeDto?> UpdateUserAsync(Guid id, UserUpdateDto input);
    Task<MeDto?> AdminUpdateUserAsync(Guid id, AdminUpdateDto dto);
    Task<IdentityResult?> ChangePasswordAsync(Guid id, UpdatePasswordDto dto);
    Task<IdentityResult?> ResetPasswordAsync(Guid id, UpdatePasswordDto dto);
}

public class UserService(
    ReviewContext context,
    UserManager<ApplicationUser> userManager,
    ILogger<UserService> logger
) : IUserService
{
    public async Task<UserDto?> GetUserByIdAsync(Guid id)
    {
        logger.LogInformation("Get user by id: {id}", id);

        var entry = await context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        if (entry is null)
            return null;

        return new UserDto
        {
            Id = entry.Id,
            UserName = entry.UserName!,
            PhoneNumber = entry.PhoneNumber!,
            Email = entry.Email!,
            Role = entry.Role,
            FirstTimeLogin = entry.FirstTimeLogin
        };
    }

    public async Task<PaginatedResult<UserDto>> GetAllUsersAsync(UserQuery filters)
    {
        logger.LogInformation("Get all users with filters: {filters}", filters);

        var query = context.Users.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(filters.Term))
            query = query.Where(u =>
                (u.UserName != null && u.UserName.Contains(filters.Term))
                || (u.Email != null && u.Email.Contains(filters.Term))
            );

        var entries = await query
            .AddPagination(filters.Offset, filters.Limit)
            .Select(s => new UserDto
            {
                Id = s.Id,
                UserName = s.UserName!,
                Email = s.Email!,
                PhoneNumber = s.PhoneNumber!,
                Role = s.Role,
            })
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

    public async Task<MeDto?> UpdateUserAsync(Guid id, UserUpdateDto input)
    {
        logger.LogInformation("Update user {id} to {dto}", id, input);

        var user = await context.Users.FindAsync(id);

        if (user is null)
            return null;

        user.Update(input);

        await context.SaveChangesAsync();

        return new MeDto
        {
            UserName = user.UserName!,
            PhoneNumber = user.PhoneNumber!,
            Email = user.Email!,
            Role = user.Role
        };
    }

    public async Task<MeDto?> AdminUpdateUserAsync(Guid id, AdminUpdateDto dto)
    {
        logger.LogInformation("Update user {id} as Admin, with {dto}", id, dto);

        var entry = await context.Users.FindAsync(id);

        if (entry is null)
            return null;

        entry.Role = dto.Role;

        await context.SaveChangesAsync();

        return new MeDto
        {
            UserName = entry.UserName!,
            PhoneNumber = entry.PhoneNumber!,
            Email = entry.Email!,
            Role = entry.Role
        };
    }

    public async Task<IdentityResult?> ChangePasswordAsync(Guid id, UpdatePasswordDto dto)
    {
        logger.LogInformation("Change password for user: {id}", id);

        var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);

        if (user is null)
            return null;

        var result = await userManager.ChangePasswordAsync(
            user,
            dto.CurrentPassword,
            dto.NewPassword
        );

        return result;
    }

    public async Task<IdentityResult?> ResetPasswordAsync(Guid id, UpdatePasswordDto dto)
    {
        logger.LogInformation("Reset password for user: {id}", id);

        var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);

        if (user is null)
            return null;

        dto.PasswordToken = await userManager.GeneratePasswordResetTokenAsync(user);
        var result = await userManager.ResetPasswordAsync(user, dto.PasswordToken, dto.NewPassword);

        return result;
    }
}
