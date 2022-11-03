using Microsoft.EntityFrameworkCore;

namespace EShopAPI.DataAccess;

public class EShopDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderStatus> OrderStatuses { get; set; }

    public EShopDbContext(DbContextOptions<EShopDbContext> options) : base(options)
    {

    }
}
