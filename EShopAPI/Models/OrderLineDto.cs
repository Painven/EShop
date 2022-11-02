namespace EShopAPI.Models;

public record OrderLineDto
{
    public int Quantity { get; init; }
    public int ProductId { get; init; }
}
