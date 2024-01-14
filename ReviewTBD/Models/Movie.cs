using ReviewTBDAPI.Contracts;

namespace ReviewTBDAPI.Models;

public class Movie
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string CoverUrl { get; set; }
    public DateOnly CreatedDate { get; set;}
    public Guid MovieStudioId { get; set; }
    public Studio? MovieStudio { get; set; }

    public MovieDto ToDto() => new()
    {
        Id = Id,
        Title = Title,
        Description = Description,
        CoverUrl = CoverUrl,
        CreatedDate = CreatedDate,
        MovieStudioId = MovieStudioId,
        MovieStudio = MovieStudio?.ToDto(),
    };
}
