using Microsoft.EntityFrameworkCore;
using ReviewTBDAPI.Contracts;
using ReviewTBDAPI.Contracts.Queries;
using ReviewTBDAPI.Utilities;

namespace ReviewTBDAPI.Services;

public interface IUserService
{
    Task<MeDto?> GetUserByIdAsync(Guid id);

    Task<PaginatedResult<UserDto>> GetAllUsersAsync(UserQuery filters);
}

public class UserService(
    ReviewContext context,
    ILogger<UserService> logger) : IUserService
{
    public async Task<MeDto?> GetUserByIdAsync(Guid id)
    {
        logger.LogInformation("Get user by id: {id}", id);

        var entry = await context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (entry is null)
        {
            return null;
        }

        return new MeDto
        {
            UserName = entry.UserName!,
            PhoneNumber = entry.PhoneNumber!,
            Email = entry.Email!,
            Role = entry.Role
        };
    }

    public async Task<PaginatedResult<UserDto>> GetAllUsersAsync(UserQuery filters)
    {
        logger.LogInformation("Get all users with filters: {filters}", filters);

        var query = context.Users.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(filters.Term))
            query = query.Where(u =>
                (u.UserName != null && u.UserName.Contains(filters.Term)) ||
                (u.Email != null && u.Email.Contains(filters.Term)));

        var entries = await query
            .AddPagination(filters.Offset, filters.Limit)
            .Select(s => new UserDto
            {
                Id = s.Id,
                UserName = s.UserName!,
                Email = s.Email!,
                PhoneNumber = s.PhoneNumber!
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
}