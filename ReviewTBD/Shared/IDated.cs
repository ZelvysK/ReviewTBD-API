namespace ReviewTBDAPI.Shared;

public interface IDated
{
    DateOnly CreatedDate { get; set; }
}
