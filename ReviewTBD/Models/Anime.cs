using ReviewTBDAPI.Contracts;
using ReviewTBDAPI.Shared;

namespace ReviewTBDAPI.Models;

public class Anime : IReleased
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string CoverImageUrl { get; set; }
    public DateOnly ReleaseDate { get; set; }
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
        ReleaseDate = ReleaseDate,
    };
}