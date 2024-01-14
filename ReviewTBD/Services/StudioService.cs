using Microsoft.EntityFrameworkCore;
using ReviewTBDAPI.Contracts;
using ReviewTBDAPI.Enums;

namespace ReviewTBDAPI.Services;

public interface IStudioService
{
    Task<StudioDto[]> GetAllStudiosAsync();
    //Task<StudioDto[]> GetAllGameStudiosAsync();
    //Task<StudioDto[]> GetAllMovieStudiosAsync();
    Task<StudioDto?> GetStudioByIdAsync(StudioType studioType, Guid id);
    //Task<StudioDto?> GetGameStudioByIdAsync(Guid id);
    //Task<StudioDto?> GetMovieStudioByIdAsync(Guid id);
}

public class StudioService(ReviewContext context, ILogger<StudioService> logger) : IStudioService
{
    public async Task<StudioDto[]> GetAllStudiosAsync() {
        logger.LogInformation("Get all studios");

        var entries = await context.Studios
            .AsNoTracking()
            .ToArrayAsync();

        var result = entries.Select(a => a.ToDto()).ToArray();

        return result;
    }

    public async Task<StudioDto?> GetStudioByIdAsync(StudioType studioType, Guid id) {
        logger.LogInformation("Get {studioType} by id: {id}", studioType, id);

        var entry = await context.Studios
            .AsNoTracking()
            .Where(t => t.Type == studioType)
            .FirstOrDefaultAsync(e => e.Id == id);

        var result = entry?.ToDto();

        return result;
    }

    ////Game Gets
    //public async Task<StudioDto[]> GetAllGameStudiosAsync() {
    //    logger.LogInformation("Get all game studios");

    //    var entries = await context.Studios
    //        .AsNoTracking()
    //        .ToArrayAsync();

    //    var result = entries.Select(e => e.ToDto()).ToArray();

    //    return result;
    //}

    //public async Task<StudioDto?> GetGameStudioByIdAsync(Guid id) {
    //    logger.LogInformation("Get game studio by id: {id}", id);

    //    var entry = await context.Studios
    //        .AsNoTracking()
    //        .FirstOrDefaultAsync(e => e.Id == id);

    //    var result = entry?.ToDto();

    //    return result;
    //}
    ////Movie Gets
    //public async Task<StudioDto[]> GetAllMovieStudiosAsync() {
    //    logger.LogInformation("Get all movie studios");

    //    var entries = await context.Studios
    //        .AsNoTracking()
    //        .ToArrayAsync();

    //    var result = entries.Select(e => e.ToDto()).ToArray();

    //    return result;
    //}

    //public async Task<StudioDto?> GetMovieStudioByIdAsync(Guid id) {
    //    logger.LogInformation("Get movie studio by id: {id}", id);

    //    var entry = await context.Studios
    //        .AsNoTracking()
    //        .FirstOrDefaultAsync(e => e.Id == id);

    //    var result = entry?.ToDto();

    //    return result;
    //}
}
