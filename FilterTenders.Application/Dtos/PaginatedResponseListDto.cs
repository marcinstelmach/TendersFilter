namespace FilterTenders.Application.Dtos;

public class PaginatedResponseListDto<TType>
{
    public required int PageNumber { get; init; }
    
    public required int PageSize { get; init; }
    
    public required int PageCount { get; init; }
    
    public required int TotalCount { get; init; }
    
    public required TType[] Data { get; init; }
}