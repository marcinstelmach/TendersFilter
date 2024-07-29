using Filters.Tenders.Core;
using FilterTenders.Application.Dtos;

namespace FilterTenders.Application;

public class TendersService : ITendersService
{
    private readonly ITendersRepository _tendersRepository;
    private readonly IGetTendersQueryBuilder _getTendersQueryBuilder;

    public TendersService(ITendersRepository tendersRepository, IGetTendersQueryBuilder getTendersQueryBuilder)
    {
        _tendersRepository = tendersRepository;
        _getTendersQueryBuilder = getTendersQueryBuilder;
    }

    public async Task<PaginatedResponse<Tender>> GetTendersAsync(GetTendersRequest request)
    {
        var pageSize = request.PageSize ?? 100;
        var pageNumber = request.PageNumber ?? 1;
        
        var specification = _getTendersQueryBuilder.BuildSpecificationForQuery(request);

        var tenders = (await _tendersRepository.GetTendersAsync())
            .Where(specification.ToExpression().Compile());
        var totalCount = tenders.Count();

        tenders = _getTendersQueryBuilder.ApplyOrdering(request, tenders);
        tenders = tenders
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
        
        return new PaginatedResponse<Tender>
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount,
            Data = tenders.ToArray()
        };
    }
}