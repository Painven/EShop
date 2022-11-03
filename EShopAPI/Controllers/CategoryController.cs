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
    public ActionResult<CategoryDetailsDto[]> GetAll()
    {
        using var db = dbFactory.CreateDbContext();

        try
        {
            var categories = db.Categories.Include(p => p.Products)
            .OrderBy(c => c.Id)
            .Select(x => mapper.Map<CategoryDetailsDto>(x))
            .ToArray();

            categories.ToList().ForEach(c => c.Products = null);

            return Ok(categories);
        }
        catch(Exception ex)
        {

        }

        return NoContent();
    }

    [HttpGet("{id}")]
    public ActionResult<CategoryDetailsDto> GetCategoryById(int id, int page = 1, int pageSize = 32)
    {
        using var db = dbFactory.CreateDbContext();

        try
        {
            int totalProducts = db.Products.Where(p => p.CategoryId == id).Count();

            var category = db.Categories
                .Include(c => c.Products.OrderBy(p => p.Id).Skip((page - 1) * pageSize).Take(pageSize))
                .SingleOrDefault(c => c.Id == id);

            var dto = mapper.Map<CategoryDetailsDto>(category);
            dto.ProductsInCategory = totalProducts;

            if (dto != null)
            {
                return Ok(dto);
            }
        }
        catch(Exception ex)
        {

        }

        return NotFound();
    }

    [HttpPost]
    public ActionResult CreateNew([FromBody] CategoryDto category)
    {
        using var db = dbFactory.CreateDbContext();
        try
        {
            Category newCategory = mapper.Map<Category>(category);

            db.Categories.Add(newCategory);

            return db.SaveChanges() >= 1 ? Ok() : BadRequest();

        }
        catch (Exception ex)
        {
            
        }

        return BadRequest();
    }

    [HttpPut]
    public ActionResult UpdateCategory([FromBody] CategoryDto category)
    {
        using var db = dbFactory.CreateDbContext();

        var dbCategory = db.Categories.Find(category.Id);

        if (dbCategory != null)
        {
            dbCategory.Name = category.Name;
            dbCategory.Image = category.Image;

            return db.SaveChanges() >= 1 ? Ok() : BadRequest();
        }

        return NotFound();

        
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteCategory(int id)
    {
        using var db = dbFactory.CreateDbContext();

        var dbCategory = db.Categories.Find(id);
        if (dbCategory != null)
        {
            db.Categories.Remove(dbCategory);
            db.SaveChanges();

            return Ok();
        }

        return NotFound();
    }
}
