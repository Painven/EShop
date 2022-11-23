using EShopAPI.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace EShopTelegramClient;

public interface IOrderStatusRepository
{
    Dictionary<string, string> GetOrderStatuses();
}

public class MemoryCachedOrderStatusRepository : IOrderStatusRepository
{
    private readonly IDbContextFactory<EShopDbContext> dbFactory;
    private readonly IMemoryCache cache;

    public TimeSpan CacheExpireTime { get; set; } = TimeSpan.FromMinutes(5);

    public MemoryCachedOrderStatusRepository(IDbContextFactory<EShopDbContext> dbFactory, IMemoryCache cache)
    {
        this.dbFactory = dbFactory;
        this.cache = cache;
    }

    public Dictionary<string, string> GetOrderStatuses()
    {
        Dictionary<string, string> ordersInfo;
        string cacheKey = "order_statuses";

        if (!cache.TryGetValue(cacheKey, out ordersInfo))
        {
            using var db = dbFactory.CreateDbContext();

            ordersInfo = db.Orders
                .Include(o => o.OrderStatus)
                .ToDictionary(i => i.Id.ToString(), i => i.OrderStatus.Name);

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(CacheExpireTime);

            cache.Set(cacheKey, ordersInfo, cacheEntryOptions);
        }

        return ordersInfo;
    }
}
