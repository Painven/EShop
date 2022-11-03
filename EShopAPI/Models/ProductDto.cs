namespace EShopAPI.Models;

public class ProductDto
{
    public int Id { get; init; }
    public DateTime Created { get; init; }
    public string Name { get; init; }
    public string Image { get; init; }
    public string? ImageSmall { get; init; }
    public decimal Price { get; init; }
    public int CategoryId { get; init; }
}