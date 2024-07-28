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

    public async Task<PaginatedResponseListDto<Tender>> GetTendersAsync(GetTendersQuery query)
    {
        var pageSize = query.PageSize ?? 100;
        var pageNumber = query.PageNumber ?? 1;
        
        var specification = _getTendersQueryBuilder.BuildTSpecificationForQuery(query);

        var tenders = (await _tendersRepository.GetTendersAsync())
            .Where(specification.ToExpression().Compile());
        var totalCount = tenders.Count();

        tenders = _getTendersQueryBuilder.ApplyOrdering(query, tenders);
        tenders = tenders
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
        
        return new PaginatedResponseListDto<Tender>
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount,
            Data = tenders.ToArray()
        };
    }
}