namespace FilterTenders.Application.Dtos;

public abstract class PaginatedRequest
{
    public int? PageSize { get; init; }

    public int? PageNumber { get; init; }

    public virtual string Validate()
    {
        if (PageSize < 1)
        {
            return "PageSize must be bigger than 0";
        }

        if (PageNumber < 1)
        {
            return "PageNumber must be bigger than 0";
        }

        return string.Empty;
    }
}