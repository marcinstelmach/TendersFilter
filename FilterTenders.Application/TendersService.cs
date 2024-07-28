using Filters.Tenders.Core;

namespace FilterTenders.Application;

public class TendersService : ITendersService
{
    private readonly ITendersRepository _tendersRepository;

    public TendersService(ITendersRepository tendersRepository)
    {
        _tendersRepository = tendersRepository;
    }

    public async Task<IEnumerable<Tender>> GetTendersAsync()
    {
        var tenders = await _tendersRepository.GetTendersAsync();
        return tenders;
    }
}