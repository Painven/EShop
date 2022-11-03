using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EShopAPI.DataAccess;

[Table(name: "order")]
public class Order
{
    [Key]
    [Column("order_id")]
    public int Id { get; set; }

    [Column("created")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    [Column("complete_date")]
    public DateTime? CompleteDate { get; set; } = DateTime.UtcNow;

    [Column("customer_name")]
    public string CustomerName { get; set; }

    [Column("customer_email")]
    public string CustomerEmail { get; set; }

    [Column("shipping_method")]
    public string ShippingMethod { get; set; }

    [Column("payment_method")]
    public string PaymentMethod { get; set; }

    [Column("is_completed")]
    public bool IsCompleted { get; set; }

    public ICollection<OrderLine> OrderLines { get; set; } = new List<OrderLine>();
}
