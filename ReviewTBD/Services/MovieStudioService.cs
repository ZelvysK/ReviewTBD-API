using Microsoft.EntityFrameworkCore;
using ReviewTBDAPI.Contracts;

namespace ReviewTBDAPI.Services;

public interface IMovieStudioService
{
    Task<MovieStudioDto[]> GetAllMovieStudiosAsync();
    Task<MovieStudioDto?> GetMovieStudioByIdAsync(Guid id);
}

public class MovieStudioService(ReviewContext context, ILogger<MovieStudioService> logger) : IMovieStudioService
{
    public async Task<MovieStudioDto[]> GetAllMovieStudiosAsync() {
        logger.LogInformation("Get all movie studios");

        var entries = await context.MovieStudios
            .AsNoTracking()
            .ToArrayAsync();

        var result = entries.Select(e => e.ToDto()).ToArray();

        return result;
    }
    public async Task<MovieStudioDto?> GetMovieStudioByIdAsync(Guid id) {
        logger.LogInformation("Get movie studio by id: {id}", id);

        var entry = await context.MovieStudios
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);

        var result = entry?.ToDto();

        return result;
    }
}
