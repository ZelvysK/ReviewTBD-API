using ReviewTBDAPI.Enums;

namespace ReviewTBDAPI.Contracts.Queries;

public record StudioQuery(StudioType? StudioType, DateOnly? From, DateOnly? To, string? Term, int Limit = 10, int Offset = 0);

public class PaginatedResult<T>
{
    public int Limit { get; set; }

    public int Offset { get; set; }

    public int Total { get; set; }

    public required T[] Result { get; set; }
}

