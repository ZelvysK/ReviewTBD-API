using ReviewTBDAPI.Contracts;
using ReviewTBDAPI.Shared;

namespace ReviewTBDAPI.Models;

public class Anime : IDated
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string CoverImageUrl { get; set; }
    public DateOnly DateCreated { get; set; }
    public Guid AnimeStudioId { get; set; }
    public Studio? AnimeStudio { get; set; }

    public AnimeDto ToDto() => new()
    {
        Id = Id,
        Title = Title,
        Description = Description,
        CoverImageUrl = CoverImageUrl,
        AnimeStudioId = AnimeStudioId,
        AnimeStudio = AnimeStudio?.ToDto(),
        DateCreated = DateCreated,
    };

    public static Anime FromDto(AnimeDto dto) => new()
    {
        Title = dto.Title,
        Description = dto.Description,
        CoverImageUrl = dto.CoverImageUrl,
        DateCreated = dto.DateCreated,
        AnimeStudioId = dto.AnimeStudioId,
    };
}