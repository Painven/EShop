namespace EShopAPI.Models;

public record CategoryDetailsDto
{
    public int Id { get; init; }
    public DateTime Created { get; init; }
    public string Name { get; init; }
    public string Image { get; init; }
    public string ImageSmall { get; init; }
    public ProductDto[] Products { get; init; }
    public int ProductsInCategory { get; set; }
}

public record CategoryDto
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Image { get; init; }
}
