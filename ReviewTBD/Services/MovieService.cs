using Microsoft.EntityFrameworkCore;
using ReviewTBDAPI.Contracts;
using ReviewTBDAPI.Contracts.Queries;
using ReviewTBDAPI.Utilities;

namespace ReviewTBDAPI.Services;

public interface IMovieService
{
    Task<PaginatedResult<MovieDto>> GetAllMoviesAsync(EntryQuery filters);
    Task<PaginatedResult<MovieDto>> GetMoviesByStudioAsync(EntryQuery filters, Guid movieStudioId);
    Task<MovieDto?> GetMovieWithStudioByIdAsync(Guid id);
}

public class MovieService(ReviewContext context, ILogger<MovieService> logger) : IMovieService
{
    public async Task<PaginatedResult<MovieDto>> GetAllMoviesAsync(EntryQuery filters) {
        logger.LogInformation("Get all Movies with filters: {FIlters}", filters);

        var query = context.Movies.AsNoTracking();

        var entries = await query
            .FilterByDateCreated(filters.From, filters.To)
            .AddPagination(filters.Offset, filters.Limit)
            .ToArrayAsync();

        var totalCount = await query.CountAsync();

        var result = entries.Select(e => e.ToDto()).ToArray();

        return new PaginatedResult<MovieDto>
        {
            Limit = filters.Limit,
            Offset = filters.Offset,
            Result = result,
            Total = totalCount,
        };
    }

    public async Task<MovieDto?> GetMovieWithStudioByIdAsync(Guid id) {
        logger.LogInformation("Get movie by id: {id}", id);

        var entry = await context.Movies
            .AsNoTracking()
            .Include(s => s.MovieStudio)
            .FirstOrDefaultAsync(e => e.Id == id);

        var result = entry?.ToDto();

        return result;
    }

    public async Task<PaginatedResult<MovieDto>> GetMoviesByStudioAsync(EntryQuery filters, Guid movieStudioId) {
        logger.LogInformation("Get movie by author: {movieStudioId}, with filters: {Filters}", movieStudioId, filters);

        var query = context.Movies.AsNoTracking();

        var entries = await query
            .Where(a => a.MovieStudioId == movieStudioId)
            .FilterByDateCreated(filters.From, filters.To)
            .AddPagination(filters.Offset, filters.Limit)
            .ToArrayAsync();

        var totalCount = await query.CountAsync();

        var result = entries.Select(b => b.ToDto()).ToArray();

        return new PaginatedResult<MovieDto>
        {
            Limit = filters.Limit,
            Offset = filters.Offset,
            Result = result,
            Total = totalCount,
        };
    }
}
