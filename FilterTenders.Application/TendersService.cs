using Filters.Tenders.Core;

namespace FilterTenders.Application;

public class TendersService : ITendersService
{
    private readonly ITendersRepository _tendersRepository;
    private readonly ITendersSpecificationBuilder _tendersSpecificationBuilder;

    public TendersService(ITendersRepository tendersRepository, ITendersSpecificationBuilder tendersSpecificationBuilder)
    {
        _tendersRepository = tendersRepository;
        _tendersSpecificationBuilder = tendersSpecificationBuilder;
    }

    public async Task<IEnumerable<Tender>> GetTendersAsync(GetTendersQuery query)
    {
        var tenders = await _tendersRepository.GetTendersAsync();
        var specification = _tendersSpecificationBuilder.BuildTenderSpecification(query);

        tenders = tenders.Where(specification.ToExpression().Compile());
        tenders = ApplyOrdering(query, tenders);

        return tenders;
    }

    private static IEnumerable<Tender> ApplyOrdering(GetTendersQuery query, IEnumerable<Tender> tenders) // move out it its own file and abstraction for isolate testing
    {
        // If we would have more than two parameters, then it would be sufficient to use https://dynamic-linq.net/
        switch (query.OrderType)
        {
            case OrderType.Asc:
                tenders = query.OrderBy switch
                {
                    OrderBy.Date => tenders.OrderBy(x => x.Date),
                    OrderBy.PriceInEuro => tenders.OrderBy(x => x.AmountInEuro),
                    _ => tenders
                };
                break;
            case OrderType.Desc:
                tenders = query.OrderBy switch
                {
                    OrderBy.Date => tenders.OrderByDescending(x => x.Date),
                    OrderBy.PriceInEuro => tenders.OrderByDescending(x => x.AmountInEuro),
                    _ => tenders
                };
                break;
        }

        return tenders;
    }
}