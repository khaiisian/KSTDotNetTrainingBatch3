using KSTDotNetTrainingBatch3.Database.AppDbContextModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSTDotNetTrainingBatch3.ConsoleApp3.EFCore;

public class SaleEFCoreService
{
    #region Read
    public void Read()
    {
        AppDbContext appDbContext = new AppDbContext();
        var lst = appDbContext.TblSales
                    .Where(x=>x.DeleteFlag == false)
                    .ToList();

        for (int i = 0; i < lst.Count; i++)
        {
            Console.WriteLine("Sale Id => " + lst[i].SaleId);
            Console.WriteLine("Product Id => " + lst[i].ProductId);
            Console.WriteLine("Quantity => " + lst[i].Quantity);
            Console.WriteLine("Price => " + lst[i].Price);
        }
    }
    #endregion

    #region Create
    public void Create()
    {
        AppDbContext appDbContext = new AppDbContext();

        var item = appDbContext.TblProducts
                    .FirstOrDefault(x => x.ProductId == 17 && x.DeleteFlag == false);
        if(item is null)
        {
            Console.WriteLine("No proudct found.");
            return;
        }

        int existingQty = item.Quantity;
        if(existingQty < 12)
        {
            Console.WriteLine("There is not enough quantity for the product");
            return;
        }

        var sale = new TblSale
        {
            ProductId = 17,
            Quantity = 12,
            Price = item.Price,
            DeleteFlag = false,
            CreatedDateTime = DateTime.Now,
        };

        appDbContext.TblSales.Add(sale);
        int result = appDbContext.SaveChanges();
        if(result <= 0)
        {
            Console.WriteLine("Sale creation failed.");
            return;
        }

        item.Quantity = existingQty - sale.Quantity;
        int result2 = appDbContext.SaveChanges();

        var message = result2 > 0 ? "Sale Creation Success." : "Sale Creation Failed";
        Console.WriteLine(message);
    }
    #endregion
}
