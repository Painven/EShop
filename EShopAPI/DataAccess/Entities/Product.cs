using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EShopAPI.DataAccess;

[Table(name: "product")]
public class Product
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("created")]
    public DateTime Created { get; set; } = DateTime.Now;

    [Required]
    [MinLength(4)]
    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Column("model")]
    public string Model { get; set; } = string.Empty;

    [Column("description")]
    public string Description { get; set; } = string.Empty;

    [Column("image")]
    public string Image { get; set; } = string.Empty;

    [Column("price")]
    public decimal Price { get; set; }

    [Column("category_id")]
    public int? CategoryId { get; set; }

    public Category Category { get; set; }
}
