using ReviewTBDAPI.Contracts;
using ReviewTBDAPI.Enums;
using ReviewTBDAPI.Shared;

namespace ReviewTBDAPI.Models;

public class Media : IDated
{
    public Guid Id { get; set; }
    public MediaType MediaType { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string CoverImageUrl { get; set; }
    public DateOnly DateCreated { get; set; }
    public Guid StudioId { get; set; }
    public Studio? Studio { get; set; }

    public DateTime DatePosted { get; set; }
    public DateTime DateModified { get; set; }

    public MediaDto ToDto() => new()
    {
        Id = Id,
        MediaType = MediaType,
        Name = Name,
        Description = Description,
        CoverImageUrl = CoverImageUrl,
        DateCreated = DateCreated,
        StudioId = StudioId,
        Studio = Studio?.ToDto(),

        DatePosted = DatePosted,
        DateModified = DateModified,
    };

    public static Media FromDto(MediaDto dto) => new()
    {
        MediaType = dto.MediaType,
        Name = dto.Name,
        Description = dto.Description,
        CoverImageUrl = dto.CoverImageUrl,
        DateCreated = dto.DateCreated,
        StudioId = dto.StudioId,
    };

    public void Update(MediaDto update) {
        MediaType = update.MediaType;
        Name = update.Name;
        Description = update.Description;
        CoverImageUrl = update.CoverImageUrl;
        DateCreated = update.DateCreated;
        StudioId = update.StudioId;
    }
}
