namespace ReviewTBDAPI.Contracts;

public class AnimeDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string CoverUrl { get; set; }
    public Guid AnimeStudioId { get; set; }
    public AnimeStudioDto? Studio { get; set; }
    public DateOnly ReleaseDate { get; set; }
}
