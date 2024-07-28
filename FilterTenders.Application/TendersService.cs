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

        var tenders = await _tendersRepository.GetTendersAsync();
        var totalCount = tenders.Count;
        var pagesCount = totalCount / pageSize;

        var enumerableTenders = tenders.Where(specification.ToExpression().Compile());
        enumerableTenders = _getTendersQueryBuilder.ApplyOrdering(query, enumerableTenders);
        enumerableTenders = enumerableTenders
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
        
        return new PaginatedResponseListDto<Tender>
        {
            PageCount = pagesCount,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount,
            Data = enumerableTenders.ToArray()
        };
    }
}