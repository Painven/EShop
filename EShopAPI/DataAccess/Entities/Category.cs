using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EShopAPI.DataAccess;

[Table(name: "category")]
public class Category
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

    [Column("image")]
    public string Image { get; set; } = string.Empty;

    public ICollection<Product> Products { get; set; } = new List<Product>();
}
