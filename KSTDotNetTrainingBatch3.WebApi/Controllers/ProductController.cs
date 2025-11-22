using KSTDotNetTrainingBatch3.Database.AppDbContextModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KSTDotNetTrainingBatch3.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public ProductController()
    {
        _dbContext = new AppDbContext();
    }

    [HttpGet]
    public IActionResult GetProducts()
    {
        var lst = _dbContext.TblProducts
            .Where(x=>!x.DeleteFlag)
            .ToList();
        return Ok(lst);
    }

    [HttpGet("{id}")]
    public IActionResult GetProduct(int id)
    {
        var item = _dbContext.TblProducts
            .Where(x=>!x.DeleteFlag)
            .FirstOrDefault(x=>x.ProductId == id);

        if(item is null)
        {
            return NotFound("No item found");
        }

        return Ok(item);
    }

    [HttpPost]
    public IActionResult CreateProduct(ProductCreateRequestDto requestDto)
    {
        var item = new TblProduct
        {
            ProductName = requestDto.ProductName,
            Price = requestDto.Price,
            Quantity = requestDto.Quantity,
            DeleteFlag = false,
            CreatedDateTime = DateTime.Now
        };

        _dbContext.TblProducts.Add(item);
        int result = _dbContext.SaveChanges();
        string msg = result > 0 ? "Create successful" : "Create failed";
        return Ok(msg);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateProduct(int id, ProductUpdateRequestDto requestDto)
    {
        var item = _dbContext.TblProducts
            .Where(x => !x.DeleteFlag)
            .FirstOrDefault(x => x.ProductId==id);

        if (item is null)
        {
            return NotFound("No item found");
        }

        item.ProductName = requestDto.ProductName;
        item.Price = requestDto.Price;
        item.Quantity = requestDto.Quantity;
        item.ModifiedDateTime = DateTime.Now;

        int result = _dbContext.SaveChanges();
        string msg = result > 0 ? "Update successful" : "Update failed";
        return Ok(msg);
    }

    [HttpPatch("{id}")]
    public IActionResult PatchProduct(int id, ProductPatchDto requestDto)
    {
        var item = _dbContext.TblProducts
            .Where(x => !x.DeleteFlag)
            .FirstOrDefault(x => x.ProductId == id);

        if (item is null)
        {
            return NotFound("No item found");
        }

        if (!string.IsNullOrEmpty(requestDto.ProductName))
        {
            item.ProductName = requestDto.ProductName;
        }
        if (requestDto.Price > 0 || requestDto.Price != null)
        {
            item.Price = requestDto.Price ?? 0;
        }
        if(requestDto.Quantity> 0 || requestDto.Quantity != null)
        {
            item.Quantity = requestDto.Quantity ?? 0;
        }
        item.ModifiedDateTime = DateTime.Now;

        int result = _dbContext.SaveChanges();
        string msg = result > 0 ? "Patch successful" : "Patch failed";
        return Ok(msg);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        var item = _dbContext.TblProducts
            .Where(x => !x.DeleteFlag)
            .FirstOrDefault(x => x.ProductId == id);

        if (item is null)
        {
            return NotFound("No item found");
        }

        item.DeleteFlag = true;
        item.ModifiedDateTime = DateTime.Now;
        
        int result = _dbContext.SaveChanges();
        string msg = result > 0 ? "Delete successful" : "Delete failed";
        return Ok(msg);
    }
}


public class ProductCreateRequestDto
{
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}

public class ProductUpdateRequestDto
{
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}

public class ProductPatchDto
{
    public string? ProductName { get; set; }
    public decimal? Price { get; set; }
    public int? Quantity { get; set; }
}
