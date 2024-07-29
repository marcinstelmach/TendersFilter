using System.Net.Http.Json;
using Filters.Tenders.Core;

namespace FilterTenders.Infrastructure;

public class TendersRepository : ITendersRepository
{
    private readonly ITendersClient _tendersClient;

    public TendersRepository(ITendersClient tendersClient)
    {
        _tendersClient = tendersClient;
    }

    public async Task<ICollection<Tender>> GetTendersAsync()
    {
        const int pageSize = 100;
        const int pagesCountToFetch = 100; // change to 100
        var tenders = new List<Tender>(pageSize * pagesCountToFetch);
        
        for (var pageNumber = 1; pageNumber <= pagesCountToFetch; pageNumber++)
        {
            var response = await _tendersClient.GetTendersAsync(pageNumber);
            if (response is not null)
            {
                tenders.AddRange(response.MapToTenders());
            }
        }

        return tenders;
    }
}