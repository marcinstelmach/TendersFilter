namespace FilterTenders.Application.Dtos;

public class GetTendersRequest : PaginatedRequest
{
    // Filter by
    public decimal? FilterByPriceInEuro { get; init; }
    public DateTime? FilterByDate { get; init; }
    public int? FilterById { get; init; }
    public int? FilterBySupplierId { get; init; }

    // Order by
    public OrderBy? OrderBy { get; init; }
    public OrderType? OrderType { get; init; }

    public override string Validate()
    {
        if (OrderType is not null && OrderBy is null)
        {
            return "OrderType parameter require OrderBy parameter to be present";
        }

        return base.Validate();
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