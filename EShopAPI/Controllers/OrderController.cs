using AutoMapper;
using EShopAPI.DataAccess;
using EShopAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EShopAPI.Controllers;

[ApiController]
[Route("api/orders")]
public class OrderController : ControllerBase
{
    private readonly IDbContextFactory<EShopDbContext> dbFactory;
    private readonly IMapper mapper;

    public OrderController(IDbContextFactory<EShopDbContext> dbFactory, IMapper mapper)
    {
        this.dbFactory = dbFactory;
        this.mapper = mapper;
    }

    [HttpGet]
    public ActionResult<OrderDto[]> GetAllOrders()
    {
        using var db = dbFactory.CreateDbContext();

        var data = db.Orders.Include(o => o.OrderLines)
            .Select(dbOrder => mapper.Map<OrderDto>(dbOrder))
            .ToArray();

        return Ok(data);
    }

    [HttpGet("{id}")]
    public ActionResult<OrderDto> GetOrderById(int id)
    {
        using var db = dbFactory.CreateDbContext();

        var dbOrder = db.Orders.SingleOrDefault(o => o.Id == id);

        if (dbOrder != null)
        {
            return Ok(mapper.Map<OrderDto>(dbOrder));
        }

        return NotFound();
    }

    [HttpPost]
    public ActionResult<OrderDto> CreateOrder([FromBody] OrderDto order)
    {
        using var db = dbFactory.CreateDbContext();

        var dbOrder = mapper.Map<Order>(order);

        db.Orders.Add(dbOrder);

        dbOrder.OrderLines.Clear(); // на всякий, что бы не проскоила цена от клиента

        foreach (var l in order.Products)
        {
            var dbProduct = db.Products.SingleOrDefault(p => p.Id == l.ProductId);
            if (dbProduct == null)
            {
                throw new ArgumentException("Невозможно добавить товар с таким ID");
            }

            OrderLine line = new OrderLine()
            {
                ProductName = dbProduct.Name,
                Price = dbProduct.Price,
                Quantity = l.Quantity,
                ProductId = l.ProductId
            };
            dbOrder.OrderLines.Add(line);
        }
        dbOrder.Created = DateTime.UtcNow;

        if (db.SaveChanges() >= 1)
        {
            return Ok(mapper.Map<OrderDto>(dbOrder));
        }

        return BadRequest();
    }

    [HttpPatch("{id}")]
    public ActionResult<OrderDto> ToogleOrderStatus(int id)
    {
        using var db = dbFactory.CreateDbContext();

        var dbOrder = db.Orders.SingleOrDefault(o => o.Id == id);

        if (dbOrder != null)
        {
            dbOrder.IsCompleted = !dbOrder.IsCompleted;
            dbOrder.CompleteDate = dbOrder.IsCompleted ? DateTime.UtcNow : (DateTime?)null;
            db.SaveChanges();

            return Ok(mapper.Map<OrderDto>(dbOrder));
        }

        return NotFound();
    }


}


