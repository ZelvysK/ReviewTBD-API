namespace ReviewTBDAPI.Shared;

public interface IDated
{
    DateOnly DateCreated { get; set; }
}
