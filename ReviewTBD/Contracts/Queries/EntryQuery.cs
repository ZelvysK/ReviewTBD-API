namespace ReviewTBDAPI.Contracts.Queries;

public record EntryQuery(DateOnly? From, DateOnly? To, int Limit = 10, int Offset = 0);

