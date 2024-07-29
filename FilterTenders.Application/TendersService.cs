using Filters.Tenders.Core;
using Filters.Tenders.Core.Specifications;
using Filters.Tenders.Core.Specifications.BuildingBlocks;
using FilterTenders.Application.Dtos;

namespace FilterTenders.Application;

public class TendersService : ITendersService
{
    private readonly ITendersRepository _tendersRepository;

    public TendersService(ITendersRepository tendersRepository)
    {
        _tendersRepository = tendersRepository;
    }

    public async Task<PaginatedResponse<Tender>> GetTendersAsync(GetTendersRequest request)
    {
        var pageSize = request.PageSize ?? 100;
        var pageNumber = request.PageNumber ?? 1;

        var specification = CreateSpecification(request);

        var tenders = (await _tendersRepository.GetTendersAsync())
            .Where(specification.ToExpression().Compile())
            .ToArray();
        
        var totalCount = tenders.Length;
        tenders = tenders
            .ApplyOrdering(request)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToArray();

        return new PaginatedResponse<Tender>
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount,
            Data = tenders
        };
    }

    private static Specification<Tender> CreateSpecification(GetTendersRequest request)
    {
        return new TenderForIdSpecification(request.FilterById)
            .And(new TenderForDateSpecification(request.FilterByDate))
            .And(new TenderForPriceInEuroSpecification(request.FilterByPriceInEuro))
            .And(new TenderForSupplierIdSpecification(request.FilterBySupplierId));
    }
}