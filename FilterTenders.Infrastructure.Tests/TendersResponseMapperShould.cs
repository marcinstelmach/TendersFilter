using AutoFixture.Xunit2;
using FilterTenders.Infrastructure;
using FluentAssertions;
using Xunit;

namespace FilterTenders.Core.Tests;

public class TendersResponseMapperShould
{
    [Theory]
    [AutoData]
    public void MapEverySingleTenderFromDataCollection(TendersResponse responseToMap)
    {
        // Act
        var result = responseToMap.MapToTenders();
        
        // Assert
        result.Should().HaveSameCount(responseToMap.Data);
    }
    
    [Theory]
    [AutoData]
    public void ReturnCorrectlyMappedTenderObject(TendersResponse.Tender tenderToMap)
    {
        // Arrange
        var responseToMap = new TendersResponse { Data = [tenderToMap] };
        var suppliers = tenderToMap.Awarded.SelectMany(y => y.Suppliers).ToArray();

        // Act
        var result = responseToMap.MapToTenders();

        // Assert
        result.Should().HaveCount(1);
        var mappedTender = result.First();
        mappedTender.Id.Should().Be(tenderToMap.Id);
        mappedTender.Date.Should().Be(tenderToMap.Date);
        mappedTender.Title.Should().Be(tenderToMap.Title);
        mappedTender.Description.Should().Be(tenderToMap.Description);
        mappedTender.AmountInEuro.Should().Be(tenderToMap.AmountInEuro);

        mappedTender.Suppliers.Should().HaveSameCount(suppliers);
        for (var i = 0; i < mappedTender.Suppliers.Length; i++)
        {
            mappedTender.Suppliers[i].Id.Should().Be(suppliers.Select(y => y.Id).ElementAt(i));
            mappedTender.Suppliers[i].Name.Should().Be(suppliers.Select(y => y.Name).ElementAt(i));
        }
    }
}