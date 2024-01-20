namespace ReviewTBDAPI.Contracts;

public class MovieDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string CoverUrl { get; set; }
    public DateOnly DateCreated { get; set; }
    public Guid MovieStudioId { get; set; }
    public StudioDto? MovieStudio { get; set; }
}
