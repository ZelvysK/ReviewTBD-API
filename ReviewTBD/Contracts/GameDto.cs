namespace ReviewTBDAPI.Contracts;

public class GameDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string CoverUrl { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public Guid GameStudioId { get; set; }
    public GameStudioDto? GameCreator { get; set; }
}
