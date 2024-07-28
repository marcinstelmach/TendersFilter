using System.Linq.Expressions;
using Filters.Tenders.Core.Specifications.BuildingBlocks;

namespace Filters.Tenders.Core.Specifications;

public class TenderForPriceInEuroSpecification : Specification<Tender>
{
    private readonly decimal? _priceInEuro;

    public TenderForPriceInEuroSpecification(decimal? priceInEuro)
    {
        _priceInEuro = priceInEuro;
    }

    public override Expression<Func<Tender, bool>> ToExpression()
    {
        if (_priceInEuro is null)
        {
            return _ => true;
        }

        return x => x.AmountInEuro == _priceInEuro;
    }
}