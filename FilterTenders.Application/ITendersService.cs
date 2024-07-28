using Filters.Tenders.Core;
using FilterTenders.Application.Queries;

namespace FilterTenders.Application;

public interface ITendersService
{
    public Task<IEnumerable<Tender>> GetTendersAsync(GetTendersQuery query);
}