using AutoFixture.Xunit2;
using Filters.Tenders.Core;
using FilterTenders.Application.Dtos;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace FilterTenders.Application.Tests;

public class TendersServiceShould
{
    private readonly ITendersRepository _tendersRepository;
    private readonly TendersService _sut;

    public TendersServiceShould()
    {
        _tendersRepository = Substitute.For<ITendersRepository>();
        _sut = new TendersService(_tendersRepository);
    }

    [Theory]
    [AutoData]
    public async Task GetTendersFromRepository(GetTendersRequest request)
    {
        // Act
        await _sut.GetTendersAsync(request);

        // Assert
        await _tendersRepository.Received(1).GetTendersAsync();
    }

    [Fact]
    public async Task ReturnFilteredOutTendersById()
    {
        // Arrange
        const int idToFiler = 1;
        var request = new GetTendersRequest { FilterById = idToFiler };
        _tendersRepository.GetTendersAsync().Returns(CreateTendersList().ToArray());

        // Act
        var result = await _sut.GetTendersAsync(request);

        // Assert
        result.Data.Should().HaveCount(1);
        var filteredOutTender = result.Data.First();
        filteredOutTender.Id.Should().Be(idToFiler);
    }

    [Fact]
    public async Task ReturnFilteredOutTendersByPriceInEuro()
    {
        // Arrange
        const decimal amountInEuro = 50.50m;
        var request = new GetTendersRequest { FilterByPriceInEuro = amountInEuro };
        _tendersRepository.GetTendersAsync().Returns(CreateTendersList().ToArray());

        // Act
        var result = await _sut.GetTendersAsync(request);

        // Assert
        result.Data.Should().HaveCount(1);
        var filteredOutTender = result.Data.First();
        filteredOutTender.Id.Should().Be(2);
        filteredOutTender.AmountInEuro.Should().Be(amountInEuro);
    }
    
    [Fact]
    public async Task ReturnFilteredOutTendersByDate()
    {
        // Arrange
        var dateToFilter = DateTime.Today.AddDays(2);
        var request = new GetTendersRequest { FilterByDate = dateToFilter };
        _tendersRepository.GetTendersAsync().Returns(CreateTendersList().ToArray());

        // Act
        var result = await _sut.GetTendersAsync(request);

        // Assert
        result.Data.Should().HaveCount(1);
        var filteredOutTender = result.Data.First();
        filteredOutTender.Id.Should().Be(3);
        filteredOutTender.Date.Should().Be(dateToFilter);
    }
    
    [Fact]
    public async Task ReturnFilteredOutTendersBySupplierId()
    {
        // Arrange
        const int supplierIdToFilter = 33;
        var request = new GetTendersRequest { FilterBySupplierId = supplierIdToFilter };
        _tendersRepository.GetTendersAsync().Returns(CreateTendersList().ToArray());

        // Act
        var result = await _sut.GetTendersAsync(request);

        // Assert
        result.Data.Should().HaveCount(1);
        var filteredOutTender = result.Data.First();
        filteredOutTender.Id.Should().Be(3);
        filteredOutTender.Suppliers.Should().Contain(x => x.Id == supplierIdToFilter);
    }
    
    [Fact]
    public async Task ReturnFilteredOutTendersByManyFilters()
    {
        // Arrange
        var request = new GetTendersRequest {FilterById = 100, FilterByDate = DateTime.Today.AddDays(-5), FilterByPriceInEuro = 999, FilterBySupplierId = 77 };
        _tendersRepository.GetTendersAsync().Returns(CreateTendersList().ToArray());

        // Act
        var result = await _sut.GetTendersAsync(request);

        // Assert
        result.Data.Should().HaveCount(1);
        var filteredOutTender = result.Data.First();
        filteredOutTender.Id.Should().Be(100);
        result.TotalCount.Should().Be(1);
    }
    
    [Fact]
    public async Task ReturnTendersOrderedByDateInAscendingDirection()
    {
        // Arrange
        var request = new GetTendersRequest {OrderBy = OrderBy.Date, OrderType = OrderType.Asc};
        _tendersRepository.GetTendersAsync().Returns(CreateTendersList().ToArray());

        // Act
        var result = await _sut.GetTendersAsync(request);

        // Assert
        result.Data.Should().HaveCount(4);
        result.Data[0].Id.Should().Be(100);
        result.Data[1].Id.Should().Be(1);
        result.Data[2].Id.Should().Be(2);
        result.Data[3].Id.Should().Be(3);
    }
    
    [Fact]
    public async Task ReturnTendersOrderedByDateInDescendingDirection()
    {
        // Arrange
        var request = new GetTendersRequest {OrderBy = OrderBy.Date, OrderType = OrderType.Desc};
        _tendersRepository.GetTendersAsync().Returns(CreateTendersList().ToArray());

        // Act
        var result = await _sut.GetTendersAsync(request);

        // Assert
        result.Data.Should().HaveCount(4);
        result.Data[0].Id.Should().Be(3);
        result.Data[1].Id.Should().Be(2);
        result.Data[2].Id.Should().Be(1);
        result.Data[3].Id.Should().Be(100);
    }
    
    [Fact]
    public async Task ReturnTendersOrderedByPriceInEuroInAscendingDirection()
    {
        // Arrange
        var request = new GetTendersRequest {OrderBy = OrderBy.PriceInEuro, OrderType = OrderType.Asc};
        _tendersRepository.GetTendersAsync().Returns(CreateTendersList().ToArray());

        // Act
        var result = await _sut.GetTendersAsync(request);

        // Assert
        result.Data.Should().HaveCount(4);
        result.Data[0].Id.Should().Be(1);
        result.Data[1].Id.Should().Be(2);
        result.Data[2].Id.Should().Be(3);
        result.Data[3].Id.Should().Be(100);
    }
    
    [Fact]
    public async Task ReturnTendersOrderedByPriceInEuroInDescendingDirection()
    {
        // Arrange
        var request = new GetTendersRequest {OrderBy = OrderBy.PriceInEuro, OrderType = OrderType.Desc};
        _tendersRepository.GetTendersAsync().Returns(CreateTendersList().ToArray());

        // Act
        var result = await _sut.GetTendersAsync(request);

        // Assert
        result.Data.Should().HaveCount(4);
        result.Data[0].Id.Should().Be(100);
        result.Data[1].Id.Should().Be(3);
        result.Data[2].Id.Should().Be(2);
        result.Data[3].Id.Should().Be(1);
    }
    
    [Fact]
    public async Task ReturnTendersPaginatedWithDefaultValuesIfNotProvided()
    {
        // Arrange
        var request = new GetTendersRequest();
        _tendersRepository.GetTendersAsync().Returns(CreateTendersList().ToArray());
        const int expectedPageSize = 100;
        const int expectedPageNumber = 1;

        // Act
        var result = await _sut.GetTendersAsync(request);

        // Assert
        result.Data.Should().HaveCountLessThan(expectedPageSize);
        result.PageSize.Should().Be(expectedPageSize);
        result.PageNumber.Should().Be(expectedPageNumber);
    }
    
    [Fact]
    public async Task ReturnTendersPaginatedCorrectly()
    {
        // Arrange
        var request = new GetTendersRequest{PageSize = 2, PageNumber = 2};
        _tendersRepository.GetTendersAsync().Returns(CreateTendersList().ToArray());

        // Act
        var result = await _sut.GetTendersAsync(request);

        // Assert
        result.Data.Should().HaveCount(2);
        result.Data[0].Id.Should().Be(3);
        result.Data[1].Id.Should().Be(100);
        result.PageSize.Should().Be(2);
        result.PageNumber.Should().Be(2);
    }
    

    private static IEnumerable<Tender> CreateTendersList()
    {
        yield return new Tender { Id = 1, AmountInEuro = 20.20m, Date = DateTime.Today, Description = "Description", Title = "Title", Suppliers = [new Supplier { Id = 11, Name = "X" }] };
        yield return new Tender { Id = 2, AmountInEuro = 50.50m, Date = DateTime.Today.AddDays(1), Description = "Description", Title = "Title", Suppliers = [new Supplier { Id = 22, Name = "Y" }] };
        yield return new Tender { Id = 3, AmountInEuro = 80.80m, Date = DateTime.Today.AddDays(2), Description = "Description", Title = "Title", Suppliers = [new Supplier { Id = 33, Name = "Z" }] };
        yield return new Tender { Id = 100, AmountInEuro = 999m, Date = DateTime.Today.AddDays(-5), Description = "Description", Title = "Title", Suppliers = [new Supplier { Id = 77, Name = "Z" }] };
    }
}