using System.ComponentModel.DataAnnotations;
using AutoFixture.Xunit2;
using FilterTenders.Application.Dtos;
using FluentAssertions;
using Xunit;

namespace FilterTenders.Application.Tests;

public class GetTendersRequestShould
{
    [Fact]
    public void ReturnErrorWhenOrderTypeWasProvidedWithoutOrderBy()
    {
        // Arrange
        var sut = new GetTendersRequest { OrderType = OrderType.Asc };
        
        // Act
        var result = sut.Validate();
        
        // Assert
        result.Should().Be("OrderType parameter require OrderBy parameter to be present");
    }
    
    [Theory]
    [AutoData]
    public void ReturnErrorWhenPageSizeIsLowerThanOne([Range(-10, 0)]int pageSize)
    {
        // Arrange
        var sut = new GetTendersRequest { PageSize = pageSize};
        
        // Act
        var result = sut.Validate();
        
        // Assert
        result.Should().Be("PageSize must be bigger than 0");
    }
    
    [Theory]
    [AutoData]
    public void ReturnErrorWhenPageNumberIsLowerThanOne([Range(-10, 0)]int pageNumber)
    {
        // Arrange
        var sut = new GetTendersRequest { PageNumber = pageNumber};
        
        // Act
        var result = sut.Validate();
        
        // Assert
        result.Should().Be("PageNumber must be bigger than 0");
    }
    
    [Theory]
    [AutoData]
    public void ReturnEmptyStringWhenRequestIsCorrect([Range(1, 100)]int pageNumber, [Range(1, 100)]int pageSize)
    {
        // Arrange
        var sut = new GetTendersRequest { PageNumber = pageNumber, PageSize = pageSize, OrderBy = OrderBy.Date, OrderType = OrderType.Desc};
        
        // Act
        var result = sut.Validate();
        
        // Assert
        result.Should().BeEmpty();
    }
}