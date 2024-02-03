using ReviewTBDAPI.Contracts;
using ReviewTBDAPI.Shared;


namespace ReviewTBDAPI.Models;

public class Game : IDated
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string CoverImageUrl { get; set; }
    public DateOnly DateCreated { get; set;}
    public Guid GameCreatorId { get; set; }
    public Studio? GameCreator{ get; set; }

    public GameDto ToDto() => new()
    {
        Id = Id,
        Title = Title,
        Description = Description,
        CoverImageUrl = CoverImageUrl,
        DateCreated = DateCreated,
        GameCreatorId = GameCreatorId,
        GameCreator = GameCreator?.ToDto(),
    };

    public static Game FromDto(GameDto dto) => new()
    {
        Title = dto.Title,
        Description = dto.Description,
        CoverImageUrl = dto.CoverImageUrl,
        DateCreated = dto.DateCreated,
        GameCreatorId = dto.GameCreatorId,
    };
}
