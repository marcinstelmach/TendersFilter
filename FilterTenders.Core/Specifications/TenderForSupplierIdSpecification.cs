using System.Linq.Expressions;
using Filters.Tenders.Core.Specifications.BuildingBlocks;

namespace Filters.Tenders.Core.Specifications;

public class TenderForSupplierIdSpecification : Specification<Tender>
{
    private readonly int? _supplierId;

    public TenderForSupplierIdSpecification(int? supplierId)
    {
        _supplierId = supplierId;
    }

    public override Expression<Func<Tender, bool>> ToExpression()
    {
        if (_supplierId is null)
        {
            return _ => true;
        }

        return x => x.Suppliers.Any(y => y.Id == _supplierId);
    }
}