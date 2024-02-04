using Microsoft.EntityFrameworkCore;
using ReviewTBDAPI.Contracts;
using ReviewTBDAPI.Contracts.Queries;
using ReviewTBDAPI.Models;
using ReviewTBDAPI.Utilities;

namespace ReviewTBDAPI.Services;

public interface IGameService
{
    Task<PaginatedResult<GameDto>> GetAllGamesAsync(EntryQuery filters);
    Task<PaginatedResult<GameDto>> GetGamesByCreatorAsync(EntryQuery filters, Guid gameCreatorId);
    Task<GameDto?> GetGameWithCreatorByIdAsync(Guid id);
    Task<Guid> CreateGameAsync(GameDto gameDto);
}

public class GameService(ReviewContext context, ILogger<GameService> logger) : IGameService
{
    public async Task<PaginatedResult<GameDto>> GetAllGamesAsync(EntryQuery filters) {
        logger.LogInformation("Get all games, filters: {Filters}", filters);

        var query = context.Games.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(filters.Term))
        {
            query = query.Where(s => s.Title.Contains(filters.Term) || s.Description.Contains(filters.Term));
        }

        var entries = await query
            .FilterByDateCreated(filters.From, filters.To)
            .AddPagination(filters.Offset, filters.Limit)
            .ToArrayAsync();

        var totalCount = await query.CountAsync();

        var result = entries.Select(e => e.ToDto()).ToArray();

        return new PaginatedResult<GameDto>
        {
            Limit = filters.Limit,
            Offset = filters.Offset,
            Result = result,
            Total = totalCount
        };
    }

    public async Task<PaginatedResult<GameDto>> GetGamesByCreatorAsync(EntryQuery filters, Guid gameCreatorId) {
        logger.LogInformation("Get game by creator: {gameCreatorId} with filters: {Filters}", gameCreatorId, filters);

        var query = context.Games.AsNoTracking();

        var entries = await query
            .Where(a => a.GameCreatorId == gameCreatorId)
            .FilterByDateCreated(filters.From, filters.To)
            .AddPagination(filters.Offset, filters.Limit)
            .ToArrayAsync();

        var totalCount = await query.CountAsync();

        var result = entries.Select(b => b.ToDto()).ToArray();

        return new PaginatedResult<GameDto>
        {
            Limit = filters.Limit,
            Offset = filters.Offset,
            Result = result,
            Total = totalCount
        };
    }

    public async Task<GameDto?> GetGameWithCreatorByIdAsync(Guid id) {
        logger.LogInformation("Get game by id: {id}", id);

        var entry = await context.Games
            .AsNoTracking()
            .Include(s => s.GameCreator)
            .FirstOrDefaultAsync(e => e.Id == id);

        var result = entry?.ToDto();

        return result;
    }

    public async Task<Guid> CreateGameAsync(GameDto gameDto) {

        var game = Game.FromDto(gameDto);

        context.Games.Add(game);

        await context.SaveChangesAsync();

        return game.Id;
    }
}
