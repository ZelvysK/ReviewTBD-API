using ReviewTBDAPI.Enums;

namespace ReviewTBDAPI.Contracts;

public class StudioDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public DateOnly FoundedDate { get; set; }
    public StudioType Type { get; set; }
}
