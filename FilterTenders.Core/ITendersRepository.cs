namespace Filters.Tenders.Core;

public interface ITendersRepository
{
    Task<IEnumerable<Tender>> GetTendersAsync();
}