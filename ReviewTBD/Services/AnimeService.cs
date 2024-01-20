using Microsoft.EntityFrameworkCore;
using ReviewTBDAPI.Contracts;
using ReviewTBDAPI.Contracts.Queries;
using ReviewTBDAPI.Utilities;


namespace ReviewTBDAPI.Services;

public interface IAnimeService
{
    Task<PaginatedResult<AnimeDto>> GetAllAnimesAsync(EntryQuery filters);
    Task<PaginatedResult<AnimeDto>> GetAnimeByStudioAsync(EntryQuery filters,Guid animeStudioId);
    Task<AnimeDto?> GetAnimeWithStudioByIdAsync(Guid id);
}

public class AnimeService(ReviewContext context, ILogger<AnimeService> logger) : IAnimeService
{
    public async Task<PaginatedResult<AnimeDto>> GetAllAnimesAsync(EntryQuery filters) {
        logger.LogInformation("Get all Anime, filters: {Filters}", filters);

        var query = context.Animes.AsNoTracking();

        var entries = await query
            .FilterByDateCreated(filters.From, filters.To)
            .AddPagination(filters.Offset, filters.Limit)
            .ToArrayAsync();

        var totalCount = await query.CountAsync();

        var result = entries.Select(a => a.ToDto()).ToArray();

        return new PaginatedResult<AnimeDto>
        {
            Limit = filters.Limit,
            Offset = filters.Offset,
            Result = result,
            Total = totalCount
        }; ;
    }

    public async Task<AnimeDto?> GetAnimeWithStudioByIdAsync(Guid id) {
        logger.LogInformation("Get anime by id: {id}", id);

        var entry = await context.Animes
            .AsNoTracking()
            .Include(a => a.AnimeStudio)
            .FirstOrDefaultAsync(e => e.Id == id);

        var result = entry?.ToDto();

        return result;
    }

    public async Task<PaginatedResult<AnimeDto>> GetAnimeByStudioAsync(EntryQuery filters, Guid animeStudioId) {
        logger.LogInformation("Get Anime by author: {animeStudioId}, with filters: {Filters}", animeStudioId, filters);

        var query = context.Animes.AsNoTracking();

        var entries = await query
            .Where(a => a.AnimeStudioId == animeStudioId)
            .FilterByDateCreated(filters.From, filters.To)
            .AddPagination(filters.Offset, filters.Limit)
            .ToArrayAsync();

        var totalCount = await query.CountAsync();

        var result = entries.Select(b => b.ToDto()).ToArray();

        return new PaginatedResult<AnimeDto>
        {
            Limit = filters.Limit,
            Offset = filters.Offset,
            Result = result,
            Total = totalCount
        }; ;
    }
}
