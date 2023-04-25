using AutoMapper;
using EShopAPI.DataAccess;
using EShopAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EShopAPI.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly IDbContextFactory<EShopDbContext> dbFactory;
    private readonly IMapper mapper;
    const int SEARCH_LIMIT = 50;

    public ProductController(IDbContextFactory<EShopDbContext> dbFactory, IMapper mapper)
    {
        this.dbFactory = dbFactory;
        this.mapper = mapper;
    }

    [HttpGet]
    public ActionResult<ProductDto[]> GetAll()
    {
        using var db = dbFactory.CreateDbContext();

        var products = db.Products
            .OrderByDescending(p => p.Id)
            .AsEnumerable()
            .Select(x => mapper.Map<ProductDto>(x))
            .ToArray();

        if (products.Length > 0)
        {
            return Ok(products);
        }
        return NoContent();
    }

    [HttpGet("bestsellers")]
    public ActionResult<ProductDto[]> GetBestsellersProducts([FromQuery] int itemsCount = 4)
    {
        if (itemsCount <= 0 || itemsCount >= 32)
        {
            return NoContent();
        }

        using var db = dbFactory.CreateDbContext();

        var products = db.Products
            .OrderByDescending(p => p.Price >= 10000 && p.Price <= 20000 && p.Name.Length <= 32)
            .Take(itemsCount)
            .Select(x => mapper.Map<ProductDto>(x))
            .ToArray();

        if (products.Length > 0)
        {
            return Ok(products);
        }
        return NoContent();
    }

    [HttpGet("{id}")]
    public ActionResult<ProductDto> Get(int id)
    {
        using var db = dbFactory.CreateDbContext();

        var dbProduct = db.Products.SingleOrDefault(p => p.Id == id);

        if (dbProduct != null)
        {
            var dto = mapper.Map<ProductDto>(dbProduct);
            return Ok(dto);
        }

        return NotFound();
    }

    [HttpPost]
    public ActionResult CreateNew([FromBody] ProductDto product)
    {
        using var db = dbFactory.CreateDbContext();
        try
        {
            Product newProduct = mapper.Map<Product>(product);

            db.Products.Add(newProduct);

            return db.SaveChanges() >= 1 ? Ok() : BadRequest();
        }
        catch (Exception ex)
        {

        }
        return BadRequest();

    }

    [HttpDelete("{id}")]
    public ActionResult DeleteProduct(int id)
    {
        using var db = dbFactory.CreateDbContext();

        var dbProduct = db.Products.Find(id);
        if (dbProduct != null)
        {
            db.Products.Remove(dbProduct);
            db.SaveChanges();

            return Ok();
        }
        return NotFound();
    }

    [HttpPut("{Id}")]
    public ActionResult UpdateProduct([FromBody] ProductDto product)
    {
        using var db = dbFactory.CreateDbContext();

        var dbProduct = db.Products.Find(product.Id);
        if (dbProduct != null)
        {
            dbProduct.Name = product.Name;
            dbProduct.Image = product.Image;
            dbProduct.Price = product.Price;
            dbProduct.CategoryId = product.CategoryId;
            db.SaveChanges();

            return Ok();
        }

        return NotFound();
    }

    [HttpGet("search/{text}")]
    public ActionResult<ProductDto[]> SearchProducts(string text)
    {
        if (string.IsNullOrWhiteSpace(text) || text.Length < 3)
        {
            return BadRequest();
        }

        using var db = dbFactory.CreateDbContext();
        string textLower = text.ToLower();

        var products = db.Products
            .Where(product => product.Name.ToLower().Contains(textLower))
            .Select(x => mapper.Map<ProductDto>(x))
            .Take(SEARCH_LIMIT)
            .ToArray();

        if (products.Length > 0)
        {
            return products;
        }

        return NoContent();
    }
}
