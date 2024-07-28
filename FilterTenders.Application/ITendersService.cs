using Filters.Tenders.Core;

namespace FilterTenders.Application;

public interface ITendersService
{
    public Task<IEnumerable<Tender>> GetTendersAsync(GetTendersQuery query);
}