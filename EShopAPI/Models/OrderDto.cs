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
    public string OrderStatus { get; init; }
    public DateTime? CompleteDate { get; init; }
    public OrderLineDto[] Products { get; init; }
}

public record OrderLineDto
{
    public int Quantity { get; init; }
    public int ProductId { get; init; }
    public string ProductName { get; init; }
    public string Price { get; init; }
}

public record CreateOrderDto
{
    public string CustomerName { get; init; }
    public string CustomerEmail { get; init; }
    public string ShippingMethod { get; init; }
    public string PaymentMethod { get; init; }
    public CreateOrderProductLineDto[] Products { get; init; }
}

public record CreateOrderProductLineDto
{
    public int ProductId { get; init; }
    public int Quantity { get; init; }
}