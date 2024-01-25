using ReviewTBDAPI.Contracts;
using ReviewTBDAPI.Enums;
using ReviewTBDAPI.Shared;

namespace ReviewTBDAPI.Models;

public class Studio : IDated
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public DateOnly DateCreated { get; set; }
    public StudioType Type { get; set; }

    public StudioDto ToDto() => new()
    {
        Id = Id,
        Name = Name,
        Description = Description,
        ImageUrl = ImageUrl,
        DateCreated = DateCreated,
        Type = Type,
    };

    public static Studio FromDto(StudioDto dto) => new()
    {
        Name = dto.Name,
        Description = dto.Description,
        ImageUrl = dto.ImageUrl,
        DateCreated = dto.DateCreated,
        Type = dto.Type,
    };
}
