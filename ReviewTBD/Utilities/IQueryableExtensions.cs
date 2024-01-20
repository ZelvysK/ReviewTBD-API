using ReviewTBDAPI.Shared;

namespace ReviewTBDAPI.Utilities;

public static class QueryableExtensions
{
    public static IQueryable<T> AddPagination<T>(this IQueryable<T> query, int offset, int limit) =>
        query.Skip(offset).Take(limit);

    public static IQueryable<T> FilterByDateCreated<T>(this IQueryable<T> query, DateOnly? from, DateOnly? to)
        where T : IDated
    {
        if (from.HasValue && to.HasValue && from.Value > to.Value)
        {
            throw new ArgumentException("The 'from' date must be less than or equal to the 'to' date.");
        }

        return query.Where(item =>
            (!from.HasValue || item.DateCreated >= from.Value) &&
            (!to.HasValue || item.DateCreated <= to.Value)
        );
    }
}