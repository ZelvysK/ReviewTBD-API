using Microsoft.EntityFrameworkCore;
using ReviewTBDAPI.Contracts;
using ReviewTBDAPI.Enums;

namespace ReviewTBDAPI.Services;

public interface IStudioService
{
    Task<StudioDto[]> GetAllStudiosAsync();
    Task<StudioDto?> GetStudioByIdAsync(Guid id);
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

    public async Task<StudioDto?> GetStudioByIdAsync(Guid id) {
        logger.LogInformation("Get studio by id: {id}", id);

        var entry = await context.Studios
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);

        var result = entry?.ToDto();

        return result;
    }

}
