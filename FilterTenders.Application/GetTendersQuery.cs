namespace FilterTenders.Application;

public class GetTendersQuery
{
    public GetTendersQuery()
    {
        PageSize = 100;
        PageNumber = 1;
    }
    
    // Filter by
    public decimal? FilterByPriceInEuro { get; init; }
    public DateTime? FilterByDate { get; init; }
    public int? FilterById { get; init; }
    public int? FilterBySupplierId { get; init; }

    // Order by PriceInEuro | Date | 
    public OrderBy? OrderBy { get; init; }
    public OrderType? OrderType { get; init; }

    // Pagination
    public int? PageSize { get; init; }

    public int? PageNumber { get; init; }

    public string Validate()
    {
        if (OrderType is not null && OrderBy is null)
        {
            return "OrderType parameter require OrderBy parameter to be present";
        }

        return string.Empty;
    }
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