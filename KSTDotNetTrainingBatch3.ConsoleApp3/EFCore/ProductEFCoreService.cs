using KSTDotNetTrainingBatch3.Database.AppDbContextModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSTDotNetTrainingBatch3.ConsoleApp3.EFCore;

public class ProductEFCoreService
{
    #region Read
    public void Read()
    {
        AppDbContext _appDbContext = new AppDbContext();
        var list = _appDbContext.TblProducts
                    .Where(x => x.DeleteFlag == false)
                    .ToList();

        for (int i = 0; i < list.Count; i++)
        {
            Console.WriteLine("Product Id => " + list[i].ProductId);
            Console.WriteLine("Product Name => " + list[i].ProductName);
            Console.WriteLine("Product Price => " + list[i].Price);
            Console.WriteLine("Product Quantity => " + list[i].Quantity);
        }
    }
    #endregion

    #region Create
    public void Create()
    {
        AppDbContext _appDbContext = new AppDbContext();
        var product = new TblProduct
        {
            ProductName = "New Product",
            Quantity = 500,
            Price = 8000,
            DeleteFlag = false,
            CreatedDateTime = DateTime.Now,
        };

        _appDbContext.TblProducts.Add(product);
        int result = _appDbContext.SaveChanges();

        var message = result > 0 ? "Create Successful" : "Create Failed";
        Console.WriteLine(message);
    }
    #endregion

    #region Update
    public void Update()
    {
        AppDbContext _appDbContext = new AppDbContext();
        var product = _appDbContext.TblProducts.FirstOrDefault(x => x.ProductId == 12 && x.DeleteFlag == false);
        if (product is null)
        {
            Console.WriteLine("No item found.");
            return;
        }

        product.ProductName = "Updated Item";
        product.ModifiedDateTime = DateTime.Now;
        int result = _appDbContext.SaveChanges();

        var message = result > 0 ? "Update Successful" : "Update Failed";
        Console.WriteLine(message);
    }
    #endregion

    #region Delete
    public void Delete()
    {
        AppDbContext _appDbContext = new AppDbContext();

        var product = _appDbContext.TblProducts.FirstOrDefault(x => x.ProductId == 15 && x.DeleteFlag == false);
        if (product is null)
        {
            Console.WriteLine("No item found.");
            return;
        }
        product.DeleteFlag = true;
        product.ModifiedDateTime = DateTime.Now;
        int result = _appDbContext.SaveChanges();

        var message = result > 0 ? "Delete Successful" : "Delete Failed";
        Console.WriteLine(message);
    }
    #endregion
}

