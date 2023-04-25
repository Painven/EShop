using EShopAPI.DataAccess;
using EShopAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureMyServices();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IOrderNotificator, MailKitOrderEmailNotificator>();

var app = builder.Build();
app.UseCors("_myAllowSpecificOrigins");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static class ServiceCollectionExtensions
{
    public static void ConfigureMyServices(this IServiceCollection services)
    {


        services.AddAutoMapper(typeof(Program).Assembly);

        services.AddCors(options =>
        {
            options.AddPolicy(name: "_myAllowSpecificOrigins",
                              policy =>
                              {
                                  policy
                                  .AllowAnyOrigin()
                                  .AllowAnyHeader()
                                  .AllowAnyMethod();
                              });
        });

        services.AddDbContextFactory<EShopDbContext>(options =>
        {
            options.UseNpgsql("Host=localhost;Port=5432;Database=eshop;Username=postgres;Password=bJKE4qas0gtj");
        });

    }
}
