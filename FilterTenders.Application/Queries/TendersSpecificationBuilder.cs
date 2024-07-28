using Filters.Tenders.Core;
using Filters.Tenders.Core.Specifications;
using Filters.Tenders.Core.Specifications.BuildingBlocks;

namespace FilterTenders.Application.Queries;

public class TendersSpecificationBuilder : ITendersSpecificationBuilder
{
    public Specification<Tender> BuildTenderSpecification(GetTendersQuery query)
    {
        return new TenderForIdSpecification(query.FilterById)
            .And(new TenderForDateSpecification(query.FilterByDate))
            .And(new TenderForPriceInEuroSpecification(query.FilterByPriceInEuro))
            .And(new TenderForSupplierId(query.FilterBySupplierId));
    }
}