using Filters.Tenders.Core;
using FilterTenders.Application.Queries;

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
        
        return tenders;
    }
}