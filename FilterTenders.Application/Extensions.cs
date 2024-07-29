using Filters.Tenders.Core;

namespace FilterTenders.Application;

public static class Extensions
{
    public static IEnumerable<Tender> ApplyOrdering(this IEnumerable<Tender> tenders, GetTendersRequest request)
    {
        // If we have more parameters to orderby the https://dynamic-linq.net/ could be considered
        return request.OrderType switch
        {
            OrderType.Asc => request.OrderBy switch
            {
                OrderBy.Date => tenders.OrderBy(x => x.Date),
                OrderBy.PriceInEuro => tenders.OrderBy(x => x.AmountInEuro),
                _ => tenders
            },
            OrderType.Desc => request.OrderBy switch
            {
                OrderBy.Date => tenders.OrderByDescending(x => x.Date),
                OrderBy.PriceInEuro => tenders.OrderByDescending(x => x.AmountInEuro),
                _ => tenders
            },
            _ => tenders
        };
    }
}