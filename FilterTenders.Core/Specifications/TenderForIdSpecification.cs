using System.Linq.Expressions;
using Filters.Tenders.Core.Specifications.BuildingBlocks;

namespace Filters.Tenders.Core.Specifications;

public class TenderForIdSpecification : Specification<Tender>
{
    private readonly int? _id;

    public TenderForIdSpecification(int? id)
    {
        _id = id;
    }

    public override Expression<Func<Tender, bool>> ToExpression()
    {
        if (_id is null)
        {
            return _ => true;
        }

        return x => x.Id == _id;
    }
}