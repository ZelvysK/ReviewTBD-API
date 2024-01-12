using Microsoft.EntityFrameworkCore;
using ReviewTBDAPI.Contracts;

namespace ReviewTBDAPI.Services;

public interface IGameStudioService
{
    Task<GameStudioDto[]> GetAllGameStudiosAsync();
    Task<GameStudioDto?> GetGameStudioByIdAsync(Guid id);
}

public class GameStudioService(ReviewContext context, ILogger<GameStudioService> logger) : IGameStudioService
{
    public async Task<GameStudioDto[]> GetAllGameStudiosAsync() {
        logger.LogInformation("Get all game studios");

        var entries = await context.GameStudios
            .AsNoTracking()
            .ToArrayAsync();

        var result = entries.Select(e => e.ToDto()).ToArray();

        return result;
    }

    public async Task<GameStudioDto?> GetGameStudioByIdAsync(Guid id) {
        logger.LogInformation("Get game studio by id: {id}", id);

        var entry = await context.GameStudios
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);

        var result = entry?.ToDto();

        return result;
    }
}
