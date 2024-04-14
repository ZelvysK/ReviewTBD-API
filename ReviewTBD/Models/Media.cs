using ReviewTBDAPI.Contracts;
using ReviewTBDAPI.Enums;
using ReviewTBDAPI.Shared;

namespace ReviewTBDAPI.Models;

public class Media : IDated
{
    public Guid Id { get; set; }
    public MediaType MediaType { get; set; }
    public Genre Genre { get; set; }
    public string Name { get; set; }
    public string CoverImageUrl { get; set; }
    public string Description { get; set; }
    public Guid StudioId { get; set; }
    public Studio? Studio { get; set; }
    public string PublishedBy { get; set; }

    public DateTime DatePosted { get; set; }
    public DateTime DateModified { get; set; }
    public DateOnly DateCreated { get; set; }

    public MediaDto ToDto()
    {
        return new MediaDto
        {
            Id = Id,
            MediaType = MediaType,
            Genre = Genre,
            Name = Name,
            Description = Description,
            CoverImageUrl = CoverImageUrl,
            DateCreated = DateCreated,
            StudioId = StudioId,
            Studio = Studio?.ToDto(),
            PublishedBy = PublishedBy,

            DatePosted = DatePosted,
            DateModified = DateModified
        };
    }

    public static Media FromDto(MediaDto dto)
    {
        return new Media
        {
            MediaType = dto.MediaType,
            Genre = dto.Genre,
            Name = dto.Name,
            Description = dto.Description,
            CoverImageUrl = dto.CoverImageUrl,
            DateCreated = dto.DateCreated,
            StudioId = dto.StudioId,
            PublishedBy = dto.PublishedBy,
        };
    }

    public void Update(MediaDto update)
    {
        MediaType = update.MediaType;
        Genre = update.Genre;
        Name = update.Name;
        Description = update.Description;
        CoverImageUrl = update.CoverImageUrl;
        DateCreated = update.DateCreated;
        StudioId = update.StudioId;
        PublishedBy = update.PublishedBy;
    }
}
