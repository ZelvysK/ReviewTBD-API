using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReviewTBDAPI.Contracts;
using ReviewTBDAPI.Contracts.Queries;
using ReviewTBDAPI.Models;
using ReviewTBDAPI.Utilities;

namespace ReviewTBDAPI.Services;

public interface IMediaService
{
    Task<PaginatedResult<MediaDto>> GetAllMediaAsync(EntryQuery filters);
    Task<MediaDto?> GetMediaWithStudioByIdAsync(Guid id);
    Task<PaginatedResult<MediaDto>> GetMediaByStudioAsync(EntryQuery filters, Guid studioId);
    Task<Guid> CreateMediaAsync(MediaDto mediaDto);
    Task<MediaDto?> UpdateMediaAsync(Guid id, MediaDto input);
    Task<bool> DeleteMediaAsync(Guid id);
}

public class MediaService(ReviewContext context, ILogger<MediaService> logger) : IMediaService
{
    public async Task<PaginatedResult<MediaDto>> GetAllMediaAsync(EntryQuery filters) {
        logger.LogInformation("Get all Media, filters: {Filters}", filters);

        var query = context.Media.AsNoTracking();

        if (filters.MediaType is not null)
        {
            query = query.Where(t => t.MediaType == filters.MediaType);
        }

        if (!string.IsNullOrWhiteSpace(filters.Term))
        {
            query = query.Where(m => m.Name.Contains(filters.Term) || m.Description.Contains(filters.Term));
        }

        var entries = await query
            .FilterByDateCreated(filters.From, filters.To)
            .AddPagination(filters.Offset, filters.Limit)
            .ToArrayAsync();

        var totalCount = await query.CountAsync();

        var result = entries.Select(m => m.ToDto()).ToArray();

        return new PaginatedResult<MediaDto>
        {
            Limit = filters.Limit,
            Offset = filters.Offset,
            Result = result,
            Total = totalCount
        };
    }

    public async Task<MediaDto?> GetMediaWithStudioByIdAsync(Guid id) {
        logger.LogInformation("Get media by id: {id}", id);

        var entry = await context.Media
            .AsNoTracking()
            .Include(m => m.Studio)
            .FirstOrDefaultAsync(e => e.Id == id);

        var result = entry?.ToDto();

        return result;
    }

    public async Task<PaginatedResult<MediaDto>> GetMediaByStudioAsync(EntryQuery filters, Guid studioId) {
        logger.LogInformation("Get Media by studio: {animeStudioId}, with filters: {Filters}", studioId, filters);

        var query = context.Media.AsNoTracking();

        var entries = await query
            .Where(s => s.StudioId == studioId)
            .FilterByDateCreated(filters.From, filters.To)
            .AddPagination(filters.Offset, filters.Limit)
            .ToArrayAsync();

        var totalCount = await query.CountAsync();

        var result = entries.Select(b => b.ToDto()).ToArray();

        return new PaginatedResult<MediaDto>
        {
            Limit = filters.Limit,
            Offset = filters.Offset,
            Result = result,
            Total = totalCount
        };
    }

    public async Task<Guid> CreateMediaAsync(MediaDto mediaDto) {
        var media = Media.FromDto(mediaDto);

        context.Media.Add(media);

        await context.SaveChangesAsync();

        return media.Id;
    }

    public async Task<MediaDto?> UpdateMediaAsync(Guid id, MediaDto input) {
        var existingMedia = await context.Media.FirstOrDefaultAsync(e => e.Id == id);

        if (existingMedia is null)
        {
            return null;
        }

        existingMedia.Update(input);

        await context.SaveChangesAsync();

        var result = await context.Media.FirstOrDefaultAsync(e => e.Id == id);

        return result?.ToDto();
    }

    public async Task<bool> DeleteMediaAsync(Guid id) {
        var media = await context.Media.FirstOrDefaultAsync(e => e.Id == id);

        if (media is null)
        {
            return false;
        }

        context.Media.Remove(media);

        return await context.SaveChangesAsync() > 0;
    }

}
