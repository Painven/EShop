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

    public ProductController(IDbContextFactory<EShopDbContext> dbFactory, IMapper mapper)
    {
        this.dbFactory = dbFactory;
        this.mapper = mapper;
    }

    [HttpGet]
    public ProductDto[] GetAll()
    {
        using var db = dbFactory.CreateDbContext();

        var products = db.Products
            .OrderByDescending(p => p.Id)
            .AsEnumerable()
            .Select(x => mapper.Map<ProductDto>(x))
            .ToArray();

        return products;
    }

    [HttpGet("{id}")]
    public ProductDto Get(int id)
    {
        using var db = dbFactory.CreateDbContext();

        var product = db.Products.SingleOrDefault(p => p.Id == id);

        return mapper.Map<ProductDto>(product);
    }

    [HttpPost]
    public bool CreateNew([FromBody]ProductDto product)
    {
        using var db = dbFactory.CreateDbContext();
        bool addResult = false;
        try
        {
            Product newProduct = mapper.Map<Product>(product);

            db.Products.Add(newProduct);

            addResult = db.SaveChanges() >= 1;
        }
        catch
        {
            addResult = false;
        }
        return addResult;

    }

    [HttpDelete("{id}")]
    public bool DeleteProduct(int id)
    {
        using var db = dbFactory.CreateDbContext();

        var dbProduct = db.Products.Find(id);
        if (dbProduct != null)
        {
            db.Products.Remove(dbProduct);
            return db.SaveChanges() >= 1;
        }
        return false;
    }

    [HttpPut("{id}")]
    public bool UpdateProduct([FromBody] ProductDto product)
    {
        using var db = dbFactory.CreateDbContext();

        var dbProduct = db.Products.Find(product.Id);
        if (dbProduct != null)
        {
            dbProduct.Name = product.Name;
            dbProduct.Image = product.Image;
            dbProduct.Price = product.Price;
            dbProduct.CategoryId = product.CategoryId;

            return db.SaveChanges() == 1;
        }
        return false;
    }
}
