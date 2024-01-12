using ReviewTBDAPI.Contracts;

namespace ReviewTBDAPI.Models;

public class MovieStudio
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateOnly FoundedDate { get; set; }

    public MovieStudioDto ToDto() => new()
    {
        Id = Id,
        Name = Name,
        Description = Description,
        FoundedDate = FoundedDate,
    };
}