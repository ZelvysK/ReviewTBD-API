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
        CreatedDate = DateCreated,
        GameStudioId = GameCreatorId,
        GameCreator = GameCreator?.ToDto(),
    };
}
