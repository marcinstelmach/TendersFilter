using Filters.Tenders.Core;
using Filters.Tenders.Core.Specifications;
using Filters.Tenders.Core.Specifications.BuildingBlocks;

namespace FilterTenders.Application;

public interface IGetTendersQueryBuilder
{
    public Specification<Tender> BuildTSpecificationForQuery(GetTendersRequest request);
    public IEnumerable<Tender> ApplyOrdering(GetTendersRequest request, IEnumerable<Tender> tenders);
}

public class GetTendersQueryBuilder : IGetTendersQueryBuilder
{
    public Specification<Tender> BuildTSpecificationForQuery(GetTendersRequest request)
    {
        return new TenderForIdSpecification(request.FilterById)
            .And(new TenderForDateSpecification(request.FilterByDate))
            .And(new TenderForPriceInEuroSpecification(request.FilterByPriceInEuro))
            .And(new TenderForSupplierId(request.FilterBySupplierId));
    }

    public IEnumerable<Tender> ApplyOrdering(GetTendersRequest request, IEnumerable<Tender> tenders)
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