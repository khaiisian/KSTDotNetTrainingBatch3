using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSTDotNetTrainingBatch3.ConsoleApp2;

public class ProductEFCoreService
{
    #region Read
    public void Read()
    {
        ModelFirstAppDbContext _appDbContext = new ModelFirstAppDbContext();
        var list = _appDbContext.Products
                    .Where(x=>x.DeleteFlag == false)
                    .ToList();

        for (int i = 0; i < list.Count; i++)
        {
            Console.WriteLine("Product Id => " + list[i].ProductID);
            Console.WriteLine("Product Name => " + list[i].ProductName);
            Console.WriteLine("Product Price => " + list[i].Price);
            Console.WriteLine("Product Quantity => " + list[i].Quantity);
        }
    }
    #endregion

    #region Create
    public void Create()
    {
        ModelFirstAppDbContext _appDbContext = new ModelFirstAppDbContext();
        var product = new Tbl_Product
        {
            ProductName = "New Product",
            Quantity = 500,
            Price = 8000,
            DeleteFlag = false,
            //CreatedAt = DateTime.Now,
        };

        _appDbContext.Products.Add(product);
        int result = _appDbContext.SaveChanges();

        var message = result > 0 ? "Create Successful" : "Create Failed";
        Console.WriteLine(message);
    }
    #endregion

    #region Update
    public void Update()
    {
        ModelFirstAppDbContext _appDbContext = new ModelFirstAppDbContext();
        var product = _appDbContext.Products.FirstOrDefault(x => x.ProductID == 2 && x.DeleteFlag == false);
        if(product is null)
        {
            Console.WriteLine("No item found.");
            return;
        }

        product.ProductName = "Updated Name";
        int result = _appDbContext.SaveChanges();

        var message = result > 0 ? "Update Successful" : "Update Failed";
        Console.WriteLine(message);
    }
    #endregion

    #region Delete
    public void Delete()
    {
        ModelFirstAppDbContext _appDbContext = new ModelFirstAppDbContext();

        var product = _appDbContext.Products.FirstOrDefault(x => x.ProductID == 1 && x.DeleteFlag == false);
        if (product is null)
        {
            Console.WriteLine("No item found.");
            return;
        }
        product.DeleteFlag = true;
        int result = _appDbContext.SaveChanges();

        var message = result > 0 ? "Delete Successful" : "Delete Failed";
        Console.WriteLine(message);
    }
    #endregion
}
