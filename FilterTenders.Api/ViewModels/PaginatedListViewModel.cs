﻿namespace FilterTenders.Api.ViewModels;

public class PaginatedListViewModel<T>
{
    public required int PageNumber { get; init; }
    
    public required int PageSize { get; init; }
    
    public required int PageCount { get; init; }
    
    public required int TotalCount { get; init; }
    
    public required IEnumerable<T> Data { get; init; }
}