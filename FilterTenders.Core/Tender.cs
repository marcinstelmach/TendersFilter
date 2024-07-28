using System.Text.Json.Serialization;

namespace Filters.Tenders.Core;

public class Tender
{
    public required int Id { get; init; }
    
    public required DateTime Date { get; init; }
    
    public required string Title { get; init; }
    
    public required string Description { get; init; }
    
    public required decimal AmountInEuro { get; init; }
    
    public required Supplier[] Suppliers { get; init; }
}