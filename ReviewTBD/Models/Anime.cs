using ReviewTBDAPI.Contracts;

namespace ReviewTBDAPI.Models;

public class Anime
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string CoverUrl { get; set; }
    public Guid AnimeStudioId { get; set; }
    public AnimeStudio? Studio { get; set; }
    public DateOnly ReleaseDate { get; set; }

    public AnimeDto ToDto() => new()
    {
        Id = Id,
        Title = Title,
        Description = Description,
        CoverUrl = CoverUrl,
        AnimeStudioId = AnimeStudioId,
        Studio = Studio?.ToDto(),
        ReleaseDate = ReleaseDate,
    };
}

