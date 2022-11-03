using AutoMapper;
using EShopAPI.DataAccess;
using EShopAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EShopAPI.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoryController : ControllerBase
{
    private readonly IDbContextFactory<EShopDbContext> dbFactory;
    private readonly IMapper mapper;


    public CategoryController(IDbContextFactory<EShopDbContext> dbFactory, IMapper mapper)
    {
        this.dbFactory = dbFactory;
        this.mapper = mapper;
    }

    [HttpGet]
    public CategoryDetailsDto[] GetAll()
    {
        using var db = dbFactory.CreateDbContext();

        var categories = db.Categories.Include(p => p.Products)
            .OrderBy(c => c.Id)
            .Select(x => mapper.Map<CategoryDetailsDto>(x))
            .ToArray();

        return categories;
    }

    [HttpGet("{id}")]
    public CategoryDetailsDto GetCategoryById(int id, int page = 1, int pageSize = 32)
    {
        using var db = dbFactory.CreateDbContext();

        int totalProducts = db.Products.Where(p => p.CategoryId == id).Count();

        var category = db.Categories
            .Include(c => c.Products.OrderBy(p => p.Id).Skip((page - 1) * pageSize).Take(pageSize))
            .SingleOrDefault(c => c.Id == id);

        var dto = mapper.Map<CategoryDetailsDto>(category);
        dto.ProductsInCategory = totalProducts;

        return dto;
    }

    [HttpPost]
    public bool CreateNew([FromBody] CategoryDto category)
    {
        using var db = dbFactory.CreateDbContext();
        bool addResult = false;
        try
        {
            Category newCategory = mapper.Map<Category>(category);

            db.Categories.Add(newCategory);

            addResult = db.SaveChanges() >= 1;
        }
        catch (Exception ex)
        {
            addResult = false;
        }
        return addResult;

    }

    [HttpPut]
    public bool UpdateCategory([FromBody] CategoryDto category)
    {
        using var db = dbFactory.CreateDbContext();

        var dbCategory = db.Categories.Find(category.Id);
        if (dbCategory != null)
        {
            dbCategory.Name = category.Name;
            dbCategory.Image = category.Image;

            return db.SaveChanges() >= 1;
        }
        return false;
    }

    [HttpDelete("{id}")]
    public bool DeleteCategory(int id)
    {
        using var db = dbFactory.CreateDbContext();

        var dbCategory = db.Categories.Find(id);
        if (dbCategory != null)
        {
            db.Categories.Remove(dbCategory);
            return db.SaveChanges() >= 1;
        }
        return false;
    }
}
