namespace ReviewTBDAPI.Contracts.Queries;

public record UserQuery(
    string? Term,
    int Limit = 10,
    int Offset = 0);