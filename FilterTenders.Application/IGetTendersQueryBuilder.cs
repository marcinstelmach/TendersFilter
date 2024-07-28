using Filters.Tenders.Core;
using Filters.Tenders.Core.Specifications;
using Filters.Tenders.Core.Specifications.BuildingBlocks;

namespace FilterTenders.Application;

public interface IGetTendersQueryBuilder
{
    public Specification<Tender> BuildTSpecificationForQuery(GetTendersQuery query);
    public IEnumerable<Tender> ApplyOrdering(GetTendersQuery query, IEnumerable<Tender> tenders);
}

public class GetTendersQueryBuilder : IGetTendersQueryBuilder
{
    public Specification<Tender> BuildTSpecificationForQuery(GetTendersQuery query)
    {
        return new TenderForIdSpecification(query.FilterById)
            .And(new TenderForDateSpecification(query.FilterByDate))
            .And(new TenderForPriceInEuroSpecification(query.FilterByPriceInEuro))
            .And(new TenderForSupplierId(query.FilterBySupplierId));
    }

    public IEnumerable<Tender> ApplyOrdering(GetTendersQuery query, IEnumerable<Tender> tenders)
    {
        // If we have more parameters to orderby the https://dynamic-linq.net/ could be considered
        return query.OrderType switch
        {
            OrderType.Asc => query.OrderBy switch
            {
                OrderBy.Date => tenders.OrderBy(x => x.Date),
                OrderBy.PriceInEuro => tenders.OrderBy(x => x.AmountInEuro),
                _ => tenders
            },
            OrderType.Desc => query.OrderBy switch
            {
                OrderBy.Date => tenders.OrderByDescending(x => x.Date),
                OrderBy.PriceInEuro => tenders.OrderByDescending(x => x.AmountInEuro),
                _ => tenders
            },
            _ => tenders
        };
    }
}