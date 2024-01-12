using Microsoft.EntityFrameworkCore;
using ReviewTBDAPI.Contracts;

namespace ReviewTBDAPI.Services;

public interface IGameService
{
    Task<GameDto[]> GetAllGamesAsync();
    Task<GameDto[]> GetGamesByCreatorAsync(Guid gameCreatorId);
    Task<GameDto?> GetGameWithCreatorByIdAsync(Guid id);
}

public class GameService(ReviewContext context, ILogger<GameService> logger) : IGameService
{
    public async Task<GameDto[]> GetAllGamesAsync() {
        logger.LogInformation("Get all games");

        var entries = await context.Games
            .AsNoTracking()
            .ToArrayAsync();

        var result = entries.Select(e => e.ToDto()).ToArray();

        return result;
    }

    public async Task<GameDto?> GetGameWithCreatorByIdAsync(Guid id) {
        logger.LogInformation("Get game by id: {id}", id);

        var entry = await context.Games
            .AsNoTracking()
            .Include(s=>s.GameCreator)
            .FirstOrDefaultAsync(e => e.Id == id);

        var result = entry?.ToDto();

        return result;
    }

    public async Task<GameDto[]> GetGamesByCreatorAsync(Guid gameCreatorId) {
        logger.LogInformation("Get entry by creator: {gameCreatorId}", gameCreatorId);

        var entries = await context.Games
            .AsNoTracking()
            .Where(a => a.GameCreatorId == gameCreatorId)
            .ToArrayAsync();

        var result = entries.Select(b => b.ToDto()).ToArray();

        return result;
    }
}
