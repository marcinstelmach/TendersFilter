using System.Net.Http.Json;
using Filters.Tenders.Core;

namespace FilterTenders.Infrastructure;

public class TendersRepository : ITendersRepository
{
    private readonly HttpClient _httpClient;

    public TendersRepository(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("TendersApi");
    }

    public async Task<ICollection<Tender>> GetTendersAsync()
    {
        const int pageSize = 100;
        const int pagesCountToFetch = 1; // change to 100
        var tenders = new List<Tender>(pageSize * pagesCountToFetch);
        
        for (var pageNumber = 1; pageNumber <= pagesCountToFetch; pageNumber++)
        {
            var response = await _httpClient.GetFromJsonAsync<TendersResponse>($"?page={pageNumber}");
            if (response is not null)
            {
                tenders.AddRange(response.ToTenders());
            }
        }

        return tenders;
    }
}