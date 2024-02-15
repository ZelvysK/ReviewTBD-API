using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReviewTBDAPI.Contracts;
using ReviewTBDAPI.Contracts.Queries;
using ReviewTBDAPI.Models;
using ReviewTBDAPI.Utilities;

namespace ReviewTBDAPI.Services;

public interface IStudioService
{
    Task<Guid> CreateStudioAsync(StudioDto studioDto);
    Task<bool> DeleteStudioAsync(Guid id);
    Task<PaginatedResult<StudioDto>> GetAllStudiosAsync(StudioQuery filters);
    Task<StudioDto?> GetStudioByIdAsync(Guid id);
    Task<StudioDto?> UpdateStudioAsync(Guid id, StudioDto input);
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

        if (!string.IsNullOrWhiteSpace(filters.Term))
        {
            query = query.Where(s => s.Name.Contains(filters.Term) || s.Description.Contains(filters.Term));
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

    public async Task<Guid> CreateStudioAsync(StudioDto studioDto)
    {
        var studio = Studio.FromDto(studioDto);

        context.Studios.Add(studio);

        await context.SaveChangesAsync();

        return studio.Id;
    }

    public async Task<bool> DeleteStudioAsync(Guid id)
    {
        var studio = await context.Studios.FirstOrDefaultAsync(e => e.Id == id);

        if (studio is null)
        {
            return false;
        }

        context.Studios.Remove(studio);

        return await context.SaveChangesAsync() > 0;
    }

    public async Task<StudioDto?> UpdateStudioAsync(Guid id, StudioDto input)
    {
        var existingStudio = await context.Studios.FirstOrDefaultAsync(e => e.Id == id);

        if (existingStudio is null)
        {
            return null;
        }

        existingStudio.Update(input);

        await context.SaveChangesAsync();

        var result = await context.Studios.FirstOrDefaultAsync(e => e.Id == id);

        return result?.ToDto();
    }
}