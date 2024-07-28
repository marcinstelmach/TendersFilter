namespace FilterTenders.Application.Dtos;

public class PaginatedRequestListDto<TType>
{
    public required int PageSize { get; set; }
    
    public required int PageNumber { get; set; }
    
    public required TType Query { get; set; }
}