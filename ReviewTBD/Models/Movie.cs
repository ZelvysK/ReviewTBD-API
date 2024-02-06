using ReviewTBDAPI.Contracts;
using ReviewTBDAPI.Shared;

namespace ReviewTBDAPI.Models;

public class Movie : IDated
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string CoverUrl { get; set; }
    public DateOnly DateCreated { get; set;}
    public Guid MovieStudioId { get; set; }
    public Studio? MovieStudio { get; set; }

    public MovieDto ToDto() => new()
    {
        Id = Id,
        Title = Title,
        Description = Description,
        CoverUrl = CoverUrl,
        DateCreated = DateCreated,
        MovieStudioId = MovieStudioId,
        MovieStudio = MovieStudio?.ToDto(),
    };

    public static Movie FromDto(MovieDto dto) => new()
    {
        Title = dto.Title,
        Description = dto.Description,
        CoverUrl = dto.CoverUrl,
        DateCreated = dto.DateCreated,
        MovieStudioId = dto.MovieStudioId,
    };

    public void Update(MovieDto update) {
        Title = update.Title;
        Description = update.Description;
        CoverUrl = update.CoverUrl;
        DateCreated = update.DateCreated;
    }
}
