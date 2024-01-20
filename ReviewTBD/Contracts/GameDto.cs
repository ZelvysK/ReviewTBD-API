namespace ReviewTBDAPI.Contracts;

public class GameDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string CoverImageUrl { get; set; }
    public DateOnly CreatedDate { get; set; }
    public Guid GameStudioId { get; set; }
    public StudioDto? GameCreator { get; set; }
}
