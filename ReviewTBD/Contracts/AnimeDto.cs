namespace ReviewTBDAPI.Contracts;

public class AnimeDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string CoverImageUrl { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public Guid AnimeStudioId { get; set; }
    public StudioDto? AnimeStudio { get; set; }
}
