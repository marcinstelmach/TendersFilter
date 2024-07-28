using Filters.Tenders.Core;
using Filters.Tenders.Core.Specifications.BuildingBlocks;
using FilterTenders.Application.Queries;

namespace FilterTenders.Application;

public interface ITendersSpecificationBuilder
{
    public Specification<Tender> BuildTenderSpecification(GetTendersQuery query);
}