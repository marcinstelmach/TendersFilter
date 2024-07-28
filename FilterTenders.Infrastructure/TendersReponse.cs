using System.Text.Json.Serialization;

namespace FilterTenders.Infrastructure;

public class TendersResponse
{
    [JsonPropertyName("Data")]
    public required Tender[] Data { get; init; }
    
    public class Tender
    {
        public required int Id { get; init; }
    
        public required DateTime Date { get; init; }
    
        public required string Title { get; init; }
    
        public required string? Description { get; init; }
    
        [JsonPropertyName("awarded_value_eur")]
        public required decimal AmountInEuro { get; init; }
    
        public required Awarded[] Awarded { get; init; }
    }

    public class Awarded
    {
        public required Supplier[] Suppliers { get; init; }
    }
    
    public class Supplier
    {
        public required int Id { get; init; }
    
        public required string Name { get; init; }
    }
}