using ReviewTBDAPI.Contracts;

namespace ReviewTBDAPI.Models;

public class GameStudio
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public DateOnly FoundedDate { get; set; }

    public GameStudioDto ToDto() => new()
    {
        Id = Id,
        Name = Name,
        Description = Description,
        ImageUrl = ImageUrl,
        FoundedDate = FoundedDate,
    };
}