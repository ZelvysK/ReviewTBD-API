namespace ReviewTBDAPI.Contracts;

public class MovieStudioDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateOnly FoundedDate { get; set; }
}
