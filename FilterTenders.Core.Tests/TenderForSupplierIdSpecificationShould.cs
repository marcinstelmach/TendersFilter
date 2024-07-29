using AutoFixture.Xunit2;
using Filters.Tenders.Core;
using Filters.Tenders.Core.Specifications;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace FilterTenders.Core.Tests;

public class TenderForSupplierIdSpecificationShould
{
    [Fact]
    public void BeAlwaysSatisfiedIfSupplierIdProvidedIsNull()
    {
        // Arrange
        var sut = new TenderForSupplierIdSpecification(null);

        // Act
        sut.IsSatisfiedBy(Arg.Any<Tender>()).Should().BeTrue();
    }

    [Theory]
    [AutoData]
    public void BeSatisfiedIfSupplierIdMatches(Tender tender)
    {
        // Arrange
        var sut = new TenderForSupplierIdSpecification(tender.Suppliers[0].Id);

        // Act
        sut.ToExpression().Compile().Invoke(tender).Should().BeTrue();
    }

    [Theory]
    [AutoData]
    public void NotBeSatisfiedIfSupplierIdDoesNotMatch(Tender tender, int invalidSupplierId)
    {
        // Arrange
        var sut = new TenderForSupplierIdSpecification(invalidSupplierId);

        // Act
        sut.ToExpression().Compile().Invoke(tender).Should().BeFalse();
    }
}