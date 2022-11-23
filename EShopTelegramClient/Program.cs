using EShopAPI.DataAccess;
using EShopTelegramClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;

var app = Host.CreateDefaultBuilder(args)
    //.ConfigureDefaults(args)
    .ConfigureServices((context, services) =>
    {
        services.AddMemoryCache();

        string dbConnectionString = context.Configuration.GetConnectionString("Default");
        services.AddDbContextFactory<EShopDbContext>(options =>
        {
            options.UseNpgsql(dbConnectionString);
        });

        services.AddSingleton<IOrderStatusRepository, MemoryCachedOrderStatusRepository>();

        string apikey = context.Configuration.GetSection("TelegramBotConfiguration").GetValue<string>("API_KEY");
        services.AddSingleton<ITelegramBotClient>(x => new TelegramBotClient(apikey));

        services.AddSingleton<EShopTelegramBot>();      
    })
    .Build();

var bot = app.Services.GetRequiredService<EShopTelegramBot>();
bot.Start();

await app.RunAsync();

