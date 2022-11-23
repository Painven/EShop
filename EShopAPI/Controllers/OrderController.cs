using AutoMapper;
using EShopAPI.DataAccess;
using EShopAPI.Models;
using EShopAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EShopAPI.Controllers;

[ApiController]
[Route("api/orders")]
public class OrderController : ControllerBase
{
    private readonly IDbContextFactory<EShopDbContext> dbFactory;
    private readonly IMapper mapper;
    private readonly IOrderNotificator notificator;
    const int MAX_ORDERS_PER_REQUEST = 100;

    public OrderController(IDbContextFactory<EShopDbContext> dbFactory, IMapper mapper, IOrderNotificator notificator)
    {
        this.dbFactory = dbFactory;
        this.mapper = mapper;
        this.notificator = notificator;
    }

    //TODO: добавить пагинация (сейчас просто максимум MAX_ORDERS_PER_REQUEST штук)
    [HttpGet]
    public ActionResult<OrderDto[]> GetAllOrders()
    {
        using var db = dbFactory.CreateDbContext();

        var data = db.Orders
            .Include(o => o.OrderLines).Include(o => o.OrderStatus)
            .Take(MAX_ORDERS_PER_REQUEST)
            .Select(dbOrder => mapper.Map<OrderDto>(dbOrder))
            .ToArray();

        if (data.Length > 0)
        {
            return data;
        }
        return NoContent();
    }

    [HttpGet("{id}")]
    public ActionResult<OrderDto> GetOrderById(int id)
    {
        using var db = dbFactory.CreateDbContext();

        var dbOrder = db.Orders
            .Include(o => o.OrderLines)
            .Include(o => o.OrderStatus)
            .SingleOrDefault(o => o.Id == id);

        if (dbOrder != null)
        {
            return Ok(mapper.Map<OrderDto>(dbOrder));
        }

        return NotFound();
    }

    [HttpPost]
    public ActionResult<OrderDto> CreateOrder([FromBody] CreateOrderDto order)
    {
        /*TODO: Тут может быть ошибка, что например если клиент долго сидит в корзине с товаром то у него будет одна цена
                А при доставке на сервер возьмется актуальная на текущий момент
                Нужно либо обновлять переодически через JS в LocalStorage у клиента
                Либо полностью отвергать заказ если сумма от клиента не совпадает с суммой всего заказа посчитанного на сервере */

        using var db = dbFactory.CreateDbContext();

        var dbOrder = mapper.Map<Order>(order);

        db.Orders.Add(dbOrder);

        dbOrder.OrderLines.Clear(); // на всякий, что бы не проскоила цена от клиента

        foreach (var l in order.Products)
        {
            var dbProduct = db.Products.SingleOrDefault(p => p.Id == l.ProductId);
            if (dbProduct == null)
            {
                return NotFound();
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
            var createdOrder = mapper.Map<OrderDto>(dbOrder);

            notificator.SendNewOrderNotification(order.CustomerEmail, dbOrder.Id);

            return createdOrder;
        }

        return BadRequest();
    }

    [HttpPatch("{id}")]
    public ActionResult<OrderDto> ChangeOrderStatus(int id, [FromQuery]string newStatus)
    {
        using var db = dbFactory.CreateDbContext();

        var dbOrder = db.Orders.SingleOrDefault(o => o.Id == id);
        var dbStatus = db.OrderStatuses.SingleOrDefault(o => o.Name == newStatus);

        if (dbOrder != null && dbStatus != null)
        {
            dbOrder.OrderStatus = dbStatus;
            dbOrder.CompleteDate = (dbStatus.Name == "Выполнен") ? DateTime.UtcNow : (DateTime?)null;
            db.SaveChanges();

            return Ok(mapper.Map<OrderDto>(dbOrder));
        }

        return NotFound();
    }

    [HttpGet("statuses")]
    public ActionResult<OrderStatus[]> GetOrderStatuses()
    {
        using var db = dbFactory.CreateDbContext();

        var statuses = db.OrderStatuses.OrderBy(o => o.SortOrder).ToArray();

        if(statuses.Length > 0)
        {
            return statuses;
        }
        return NoContent();
    }
}


