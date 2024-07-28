namespace Filters.Tenders.Core;

public interface ITendersRepository
{
    Task<ICollection<Tender>> GetTendersAsync();
}