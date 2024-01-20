using Microsoft.EntityFrameworkCore;
using ReviewTBDAPI.Contracts;
using ReviewTBDAPI.Contracts.Queries;
using ReviewTBDAPI.Utilities;

namespace ReviewTBDAPI.Services;

public interface IStudioService
{
    Task<PaginatedResult<StudioDto>> GetAllStudiosAsync(StudioQuery filters);
    Task<StudioDto?> GetStudioByIdAsync(Guid id);
}

public class StudioService(ReviewContext context, ILogger<StudioService> logger) : IStudioService
{
    public async Task<PaginatedResult<StudioDto>> GetAllStudiosAsync(StudioQuery filters)
    {
        logger.LogInformation("Get all studios, filters: {Filters}", filters);

        var query = context.Studios.AsNoTracking();

        if (filters.StudioType is not null)
        {
            query = query.Where(s => s.Type == filters.StudioType);
        }

        var entries = await query
            .FilterByDateCreated(filters.From, filters.To)
            .AddPagination(filters.Offset, filters.Limit)
            .ToArrayAsync();

        var totalCount = await query.CountAsync();

        var result = entries.Select(a => a.ToDto()).ToArray();

        return new PaginatedResult<StudioDto>
        {
            Limit = filters.Limit,
            Offset = filters.Offset,
            Result = result,
            Total = totalCount
        };
    }

    public async Task<StudioDto?> GetStudioByIdAsync(Guid id)
    {
        logger.LogInformation("Get studio by id: {id}", id);

        var entry = await context.Studios
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);

        var result = entry?.ToDto();

        return result;
    }
}