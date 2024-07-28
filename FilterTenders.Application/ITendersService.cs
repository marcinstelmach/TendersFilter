using Filters.Tenders.Core;
using FilterTenders.Application.Dtos;

namespace FilterTenders.Application;

public interface ITendersService
{
    public Task<PaginatedResponseListDto<Tender>> GetTendersAsync(GetTendersQuery query);
}