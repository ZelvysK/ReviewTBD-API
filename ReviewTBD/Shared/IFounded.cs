namespace ReviewTBDAPI.Shared;

public interface IFounded
{
    DateOnly FoundedDate { get; set; }
}

public interface IReleased
{
    DateOnly ReleaseDate { get; set; }
}