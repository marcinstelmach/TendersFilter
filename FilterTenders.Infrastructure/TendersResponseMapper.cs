using Filters.Tenders.Core;

namespace FilterTenders.Infrastructure;

public static class TendersResponseMapper
{
    public static Tender[] ToTenders(this TendersResponse response)
    {
        return response.Data.Select(x =>
        {
            return new Tender
            {
                Id = x.Id,
                Date = x.Date,
                Title = x.Title,
                Description = x.Description,
                AmountInEuro = x.AmountInEuro,
                Suppliers = x.Awarded
                    .SelectMany(y => y.Suppliers)
                    .Select(y => new Supplier { Id = y.Id, Name = y.Name })
                    .ToArray()
            };
        })
        .ToArray();
    }
}