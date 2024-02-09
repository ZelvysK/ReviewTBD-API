using ReviewTBDAPI.Enums;

namespace ReviewTBDAPI.Contracts.Queries;

public record EntryQuery(MediaType? MediaType, DateOnly? From, DateOnly? To, string? Term, int Limit = 10, int Offset = 0);

