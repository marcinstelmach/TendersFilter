namespace FilterTenders.Api.ViewModels;

public class PaginatedListViewModel<T>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public IEnumerable<T> Data { get; set; }
}