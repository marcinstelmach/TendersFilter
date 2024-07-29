using AutoFixture.Xunit2;
using Filters.Tenders.Core;
using Filters.Tenders.Core.Specifications;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace FilterTenders.Core.Tests;

public class TenderForIdSpecificationShould
{
    [Fact]
    public void BeAlwaysSatisfiedIfIdProvidedIsNull()
    {
        // Arrange
        var sut = new TenderForIdSpecification(null);
        
        // Act
        sut.IsSatisfiedBy(Arg.Any<Tender>()).Should().BeTrue();
    }
    
    [Theory]
    [AutoData]
    public void BeSatisfiedIfIdMatches(Tender tender)
    {
        // Arrange
        var sut = new TenderForIdSpecification(tender.Id);
        
        // Act
        sut.ToExpression().Compile().Invoke(tender).Should().BeTrue();
    }
    
    [Theory]
    [AutoData]
    public void NotBeSatisfiedIfIdDoesNotMatch(Tender tender)
    {
        // Arrange
        var sut = new TenderForIdSpecification(tender.Id + 1);
        
        // Act
        sut.ToExpression().Compile().Invoke(tender).Should().BeFalse();
    }
}