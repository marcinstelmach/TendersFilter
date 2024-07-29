using AutoFixture.Xunit2;
using Filters.Tenders.Core;
using Filters.Tenders.Core.Specifications;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace FilterTenders.Core.Tests;

public class TenderForPriceInEuroSpecificationShould
{
    [Fact]
    public void BeAlwaysSatisfiedIfPriceInEuroProvidedIsNull()
    {
        // Arrange
        var sut = new TenderForPriceInEuroSpecification(null);

        // Act
        sut.IsSatisfiedBy(Arg.Any<Tender>()).Should().BeTrue();
    }

    [Theory]
    [AutoData]
    public void BeSatisfiedIfPriceInEuroMatches(Tender tender)
    {
        // Arrange
        var sut = new TenderForPriceInEuroSpecification(tender.AmountInEuro);

        // Act
        sut.ToExpression().Compile().Invoke(tender).Should().BeTrue();
    }

    [Theory]
    [AutoData]
    public void NotBeSatisfiedIfPriceInEuroDoesNotMatch(Tender tender)
    {
        // Arrange
        var sut = new TenderForPriceInEuroSpecification(tender.AmountInEuro + 1);

        // Act
        sut.ToExpression().Compile().Invoke(tender).Should().BeFalse();
    }
}