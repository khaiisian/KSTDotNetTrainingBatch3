using KSTDotNetTrainingBatch3.Database.AppDbContextModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KSTDotNetTrainingBatch3.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductCategoryController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public ProductCategoryController()
    {
        _dbContext = new AppDbContext();
    }

    [HttpGet]
    public IActionResult GetProductCategories()
    {
        var lst = _dbContext.TblProductCategories
            .Where(x => x.DeleteFlag == false)
            .ToList();
        return Ok(lst);
    }

    [HttpGet("{id}")]
    public IActionResult GetProductCategory(int id)
    {
        var item = _dbContext.TblProductCategories
            .Where(x => x.DeleteFlag == false)
            .FirstOrDefault(x => x.ProductCategoryId == id);

        if (item is null)
        {
            return NotFound("No item found");
        }

        return Ok(item);
    }

    [HttpPost]
    public IActionResult CreateProductCategory(ProductCategoryCreateDto requestDto)
    {
        var item = new TblProductCategory
        {
            ProductCategoryCode = requestDto.ProductCategoryCode,
            ProductCategoryName = requestDto.ProductCategoryName,
            CreatedAt = DateTime.Now,
            DeleteFlag = false
        };

        _dbContext.TblProductCategories.Add(item);
        int result = _dbContext.SaveChanges();

        string msg = result > 0 ? "Create successful" : "Create failed";
        return Ok(msg);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateProductCategory(int id, ProductCategoryUpdateDto requestDto)
    {
        var item = _dbContext.TblProductCategories
            .Where(x => x.DeleteFlag == false)
            .FirstOrDefault(x => x.ProductCategoryId == id);

        if (item is null)
        {
            return NotFound("No item found");
        }

        item.ProductCategoryCode = requestDto.ProductCategoryCode;
        item.ProductCategoryName = requestDto.ProductCategoryName;
        item.ModifiedAt = DateTime.Now;

        int result = _dbContext.SaveChanges();
        string msg = result > 0 ? "Update successful" : "Update failed";
        return Ok(msg);
    }

    [HttpPatch("{id}")]
    public IActionResult PatchProductCategory(int id, ProductCategoryPatchDto requestDto)
    {
        var item = _dbContext.TblProductCategories
            .Where(x => x.DeleteFlag == false)
            .FirstOrDefault(x => x.ProductCategoryId == id);

        if (item is null)
        {
            return NotFound("No item found");
        }

        if (!string.IsNullOrEmpty(requestDto.ProductCategoryCode))
        {
            item.ProductCategoryCode = requestDto.ProductCategoryCode;
        }

        if (!string.IsNullOrEmpty(requestDto.ProductCategoryName))
        {
            item.ProductCategoryName = requestDto.ProductCategoryName;
        }

        item.ModifiedAt = DateTime.Now;

        int result = _dbContext.SaveChanges();
        string msg = result > 0 ? "Patch successful" : "Patch failed";
        return Ok(msg);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProductCategory(int id)
    {
        var item = _dbContext.TblProductCategories
            .Where(x => x.DeleteFlag == false)
            .FirstOrDefault(x => x.ProductCategoryId == id);

        if (item is null)
        {
            return NotFound("No item found");
        }

        item.DeleteFlag = true;
        item.ModifiedAt = DateTime.Now;

        int result = _dbContext.SaveChanges();
        string msg = result > 0 ? "Delete successful" : "Delete failed";
        return Ok(msg);
    }
}
public class ProductCategoryCreateDto
{
    public string? ProductCategoryCode { get; set; }
    public string? ProductCategoryName { get; set; }
}

public class ProductCategoryUpdateDto
{
    public string? ProductCategoryCode { get; set; }
    public string? ProductCategoryName { get; set; }
}

public class ProductCategoryPatchDto
{
    public string? ProductCategoryCode { get; set; }
    public string? ProductCategoryName { get; set; }
}
