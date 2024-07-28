namespace FilterTenders.Api.ViewModels;

public class GetTendersRequest
{
    // Filter by
    public decimal? FilterByPriceInEuro { get; init; }
    public DateTime? FilterByDate { get; init; }
    public int? FilterById { get; init; }
    public int? FilterBySupplierId { get; init; }
    
    // Order by PriceInEuro | Date | 
    public OrderBy? OrderBy { get; init; } 
    public OrderType? OrderType { get; init; } // Validate, OrderType can be only when OrderBy provided
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