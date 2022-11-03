using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EShopAPI.DataAccess;

[Table(name: "order_line")]
public class OrderLine
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    [Required]
    [MinLength(4)]
    [Column("product_name")]
    public string ProductName { get; set; }
    [Column("product_id")]
    public int? ProductId { get; set; }
    [Column("quantity")]
    public int Quantity { get; set; }
    [Column("price")]
    public decimal Price { get; set; }

    [Column("order_id")]
    public int OrderId { get; set; }
    public Order Order { get; set; }
}