namespace EShopAPI.Models;

public record OrderLineDto
{
    public int Quantity { get; init; }
    public int ProductId { get; init; }
    public string ProductName { get; init; }
    public string Price { get; init; }
}
