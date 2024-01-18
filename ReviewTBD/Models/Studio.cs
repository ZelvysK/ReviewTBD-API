using ReviewTBDAPI.Contracts;
using ReviewTBDAPI.Enums;
using ReviewTBDAPI.Shared;

namespace ReviewTBDAPI.Models;

public class Studio : IFounded
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public DateOnly FoundedDate { get; set; }
    public StudioType Type { get; set; }

    public StudioDto ToDto() => new()
    {
        Id = Id,
        Name = Name,
        Description = Description,
        ImageUrl = ImageUrl,
        FoundedDate = FoundedDate,
        Type = Type,
    };
}
