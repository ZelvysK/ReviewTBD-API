using Microsoft.AspNetCore.Mvc;
using ReviewTBDAPI.Contracts;
using ReviewTBDAPI.Contracts.Queries;

namespace ReviewTBDAPI.Services;

public interface IMediaService
{
    Task<PaginatedResult<MediaDto>> GetAllMediaAsync(EntryQuery filters);
    Task<PaginatedResult<MediaDto>> GetMediaByStudioAsync(EntryQuery filters, Guid studioId);
    Task<MediaDto?> GetMediaWithStudioByIdAsync(Guid id);
    Task<Guid> CreateMediaAsync(MediaDto mediaDto);
    Task<bool> DeleteMediaAsync(Guid id);
    Task<ActionResult<MediaDto?>> UpdateMediaAsync(Guid id, MediaDto input);
}

public class MediaService(ReviewContext context, ILogger<MediaService> logger) : IMediaService
{
    public Task<PaginatedResult<MediaDto>> GetAllMediaAsync(EntryQuery filters) {
        throw new NotImplementedException();
    }

    public Task<PaginatedResult<MediaDto>> GetMediaByStudioAsync(EntryQuery filters, Guid studioId) {
        throw new NotImplementedException();
    }

    public Task<MediaDto?> GetMediaWithStudioByIdAsync(Guid id) {
        throw new NotImplementedException();
    }
    
    public Task<Guid> CreateMediaAsync(MediaDto mediaDto) {
        throw new NotImplementedException();
    }

    public Task<ActionResult<MediaDto?>> UpdateMediaAsync(Guid id, MediaDto input) {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteMediaAsync(Guid id) {
        throw new NotImplementedException();
    }

}
