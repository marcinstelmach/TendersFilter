using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Json;

namespace FilterTenders.Infrastructure;

public interface ITendersClient
{
    Task<TendersResponse?> GetTendersAsync(int pageNumber);
}

[ExcludeFromCodeCoverage]
public class TendersClient : ITendersClient 
{
    private const string TendersApi = "TendersApi";
    private readonly HttpClient _httpClient;

    public TendersClient(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient(TendersApi);
    }
    
    public async Task<TendersResponse?> GetTendersAsync(int pageNumber)
    {
        return await _httpClient.GetFromJsonAsync<TendersResponse>($"?page={pageNumber}");
    }
}