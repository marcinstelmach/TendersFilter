using System.Linq.Expressions;
using Filters.Tenders.Core.Specifications.BuildingBlocks;

namespace Filters.Tenders.Core.Specifications;

public class TenderForDateSpecification : Specification<Tender>
{
    private readonly DateTime? _date;

    public TenderForDateSpecification(DateTime? date)
    {
        _date = date;
    }

    public override Expression<Func<Tender, bool>> ToExpression()
    {
        if (_date is null)
        {
            return _ => true;
        }

        return x => x.Date == _date;
    }
}