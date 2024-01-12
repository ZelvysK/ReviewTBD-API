using ReviewTBDAPI.Contracts;

namespace ReviewTBDAPI.Models;

public class Game
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string CoverUrl { get; set; }
    public DateOnly ReleaseDate { get; set;}
    public Guid GameCreatorId { get; set; }
    public GameStudio? GameCreator{ get; set; }

    public GameDto ToDto() => new()
    {
        Id = Id,
        Title = Title,
        Description = Description,
        CoverUrl = CoverUrl,
        ReleaseDate = ReleaseDate,
        GameStudioId = GameCreatorId,
        GameCreator = GameCreator?.ToDto(),
    };
}
