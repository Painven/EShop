using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EShopAPI.DataAccess;

[Table(name: "order_status")]
public class OrderStatus
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    [Required]
    [Column("name")]
    public string Name { get; set; }

    [Column("sort_order")]
    public int SortOrder { get; set; }

    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
