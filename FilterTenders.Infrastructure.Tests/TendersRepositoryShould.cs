using AutoFixture.Xunit2;
using FilterTenders.Infrastructure;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace FilterTenders.Core.Tests;

public class TendersRepositoryShould
{
    private readonly ITendersClient _tendersClient;
    private readonly TendersRepository _sut;

    public TendersRepositoryShould()
    {
        _tendersClient = Substitute.For<ITendersClient>();
        _sut = new TendersRepository(_tendersClient);
    }

    [Fact]
    public async Task GetTendersFromClient100TimesForSpecificPageNumber()
    {
        // Arrange
        var pageNumbers = Enumerable.Range(1, 100);
        
        // Act
        await _sut.GetTendersAsync();
        
        // Assert
        await _tendersClient.Received(100).GetTendersAsync(Arg.Is<int>(x => pageNumbers.Contains(x)));
    }
    
    [Theory]
    [AutoData]
    public async Task ReturnCorrectAmountOfTenders(TendersResponse response)
    {
        // Arrange
        _tendersClient.GetTendersAsync(Arg.Any<int>()).Returns(response);
        
        // Act
        var result = await _sut.GetTendersAsync();
        
        // Assert
        result.Should().HaveCount(100 * response.Data.Length);
    }
}