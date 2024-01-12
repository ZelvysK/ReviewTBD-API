using Microsoft.EntityFrameworkCore;
using ReviewTBDAPI.Contracts;

namespace ReviewTBDAPI.Services;

public interface IAnimeStudioService
{
    Task<AnimeStudioDto[]> GetAllAnimeStudiosAsync();
    Task<AnimeStudioDto?> GetAnimeStudioByIdAsync(Guid id);
}

public class AnimeStudioService(ReviewContext context, ILogger<AnimeService> logger) : IAnimeStudioService
{
    public async Task<AnimeStudioDto[]> GetAllAnimeStudiosAsync() {
        logger.LogInformation("Get all Anime studios");

        var entries = await context.AnimeStudios
            .AsNoTracking()
            .ToArrayAsync();

        var result = entries.Select(a => a.ToDto()).ToArray();

        return result;
    }

    public async Task<AnimeStudioDto?> GetAnimeStudioByIdAsync(Guid id) {
        logger.LogInformation("Get anime by id: {id}", id);

        var entry = await context.AnimeStudios
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);

        var result = entry?.ToDto();

        return result;
    }
}
