namespace FilterTenders.Application.Queries;

public class GetTendersQuery
{
    // Filter by
    public decimal? FilterByPriceInEuro { get; init; }
    public DateTime? FilterByDate { get; init; }
    public int? FilterById { get; init; }
    public int? FilterBySupplierId { get; init; }
    
    // Order by PriceInEuro | Date | 
    public OrderBy? OrderBy { get; init; } 
    public OrderType? OrderType { get; init; }
}

public enum OrderBy
{
    PriceInEuro = 1,
    Date
}

public enum OrderType
{
    Asc = 1,
    Desc
}