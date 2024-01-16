using Microsoft.EntityFrameworkCore;
using ReviewTBDAPI.Contracts;

namespace ReviewTBDAPI.Services;

public interface IMovieService
{
    Task<MovieDto[]> GetAllMoviesAsync();
    Task<MovieDto[]> GetMoviesByStudioAsync(Guid movieStudioId);
    Task<MovieDto?> GetMovieWithStudioByIdAsync(Guid id);
}

public class MovieService(ReviewContext context, ILogger<MovieService> logger) : IMovieService
{
    public async Task<MovieDto[]> GetAllMoviesAsync() {
        logger.LogInformation("Get all Movies");

        var entries = await context.Movies
            .AsNoTracking()
            .ToArrayAsync();

        var result = entries.Select(e => e.ToDto()).ToArray();

        return result;
    }

    public async Task<MovieDto?> GetMovieWithStudioByIdAsync(Guid id) {
        logger.LogInformation("Get movie by id: {id}", id);

        var entry = await context.Movies
            .AsNoTracking()
            .Include(s=>s.MovieStudio)
            .FirstOrDefaultAsync(e => e.Id == id);

        var result = entry?.ToDto();

        return result;
    }

    public async Task<MovieDto[]> GetMoviesByStudioAsync(Guid movieStudioId) {
        logger.LogInformation("Get movie by author: {movieStudioId}", movieStudioId);

        var entries = await context.Movies
            .AsNoTracking()
            .Where(a => a.MovieStudioId == movieStudioId)
            .ToArrayAsync();

        var result = entries.Select(b => b.ToDto()).ToArray();

        return result;
    }
}
