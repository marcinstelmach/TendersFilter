using AutoFixture.Xunit2;
using Filters.Tenders.Core;
using Filters.Tenders.Core.Specifications;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace FilterTenders.Core.Tests;

public class TenderForDateSpecificationShould
{
    [Fact]
    public void BeAlwaysSatisfiedIfDateProvidedIsNull()
    {
        // Arrange
        var sut = new TenderForDateSpecification(null);
        
        // Act
        sut.IsSatisfiedBy(Arg.Any<Tender>()).Should().BeTrue();
    }
    
    [Theory]
    [AutoData]
    public void BeSatisfiedIfDateMatches(Tender tender)
    {
        // Arrange
        var sut = new TenderForDateSpecification(tender.Date);
        
        // Act
        sut.ToExpression().Compile().Invoke(tender).Should().BeTrue();
    }
    
    [Theory]
    [AutoData]
    public void NotBeSatisfiedIfDateDoesNotMatch(Tender tender)
    {
        // Arrange
        var sut = new TenderForDateSpecification(tender.Date.AddSeconds(1));
        
        // Act
        sut.ToExpression().Compile().Invoke(tender).Should().BeFalse();
    }
}