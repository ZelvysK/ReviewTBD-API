using ReviewTBDAPI.Enums;

namespace ReviewTBDAPI.Contracts;

public class MediaDto
{
    public Guid Id { get; set; }
    public MediaType MediaType { get; set; }
    public Genre Genre { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string CoverImageUrl { get; set; }
    public DateOnly DateCreated { get; set; }
    public Guid StudioId { get; set; }
    public StudioDto? Studio { get; set; }
    public string PublishedBy { get; set; }

    public DateTime DatePosted { get; set; }
    public DateTime DateModified { get; set; }
}
