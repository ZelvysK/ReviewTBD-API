using ReviewTBDAPI.Contracts;

namespace ReviewTBDAPI.Models;

public class AnimeStudio
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public DateOnly FoundedDate { get; set; }

    public AnimeStudioDto ToDto() => new()
    {
        Id = Id,
        Name = Name,
        Description = Description,
        ImageUrl = ImageUrl,
        FoundedDate = FoundedDate,
    };
}
