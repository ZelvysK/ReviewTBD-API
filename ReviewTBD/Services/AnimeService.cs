using Microsoft.EntityFrameworkCore;
using ReviewTBDAPI.Contracts;

namespace ReviewTBDAPI.Services;

public interface IAnimeService
{
    Task<AnimeDto[]> GetAllAnimesAsync();
    Task<AnimeDto[]> GetAnimeByStudioAsync(Guid animeStudioId);
    Task<AnimeDto?> GetAnimeWithStudioByIdAsync(Guid id);
}

public class AnimeService(ReviewContext context, ILogger<AnimeService> logger) : IAnimeService
{
    public async Task<AnimeDto[]> GetAllAnimesAsync() {
        logger.LogInformation("Get all Anime");

        var entries = await context.Animes
            .AsNoTracking()
            .ToArrayAsync();

        var result = entries.Select(a => a.ToDto()).ToArray();

        return result;
    }

    public async Task<AnimeDto?> GetAnimeWithStudioByIdAsync(Guid id) {
        logger.LogInformation("Get anime by id: {id}", id);

        var entry = await context.Animes
            .AsNoTracking()
            .Include(a => a.Studio)
            .FirstOrDefaultAsync(e => e.Id == id);

        var result = entry?.ToDto();
        
        return result;
    }

    public async Task<AnimeDto[]> GetAnimeByStudioAsync(Guid animeStudioId) {
        logger.LogInformation("Get entry by author: {animeStudioId}", animeStudioId);

        var entries = await context.Animes
            .AsNoTracking()
            .Where(a => a.AnimeStudioId == animeStudioId)
            .ToArrayAsync();

        var result = entries.Select(b => b.ToDto()).ToArray();

        return result;
    }
}
