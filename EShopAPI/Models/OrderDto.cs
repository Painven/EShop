namespace EShopAPI.Models;

public record OrderDto
{
    public int Id { get; init; }
    public DateTime Created { get; init; }
    public string CustomerName { get; init; }
    public string CustomerEmail { get; init; }
    public string ShippingMethod { get; init; }
    public string PaymentMethod { get; init; }
    public int TotalSum { get; init; }
    public bool IsCompleted { get; init; }
    public DateTime? CompleteDate { get; init; }
    public OrderLineDto[] Products { get; init; }
}
