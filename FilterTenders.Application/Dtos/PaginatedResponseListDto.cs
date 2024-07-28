namespace FilterTenders.Application.Dtos;

public class PaginatedResponseListDto<TType>
{
    public required int PageNumber { get; init; }

    public required int PageSize { get; init; }

    public required int TotalCount { get; init; }
    
    public int PagesCount => (int)Math.Ceiling((decimal)TotalCount / PageSize);

    public bool HasPreviousPage => PageNumber < 1;

    public bool HasNextPage => PageNumber < PagesCount;

    public required TType[] Data { get; init; }
}