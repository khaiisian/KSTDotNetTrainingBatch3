using KSTDotNetTrainingBatch3.Database.AppDbContextModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSTDotNetTrainingBatch3.ConsoleApp2;

public class ProductCategoryEFCoreService
{
    #region Read
    public void Read()
    {
        AppDbContext appDbContext = new AppDbContext();
        var categories = appDbContext.TblProductCategories
                                     .Where(x => x.DeleteFlag == false)
                                     .ToList();

        for (int i = 0; i < categories.Count; i++)
        {
            Console.WriteLine("Category Id => " + categories[i].ProductCategoryId);
            Console.WriteLine("Category Name => " + categories[i].ProductCategoryName);
        }
    }
    #endregion

    #region Create
    public void Create()
    {
        AppDbContext appDbContext = new AppDbContext();
        var category = new TblProductCategory
        {
            ProductCategoryCode = "PC002",
            ProductCategoryName = "New Category",
            DeleteFlag = false,
        };

        appDbContext.TblProductCategories.Add(category);
        int result = appDbContext.SaveChanges();

        var message = result > 0 ? "Create Successful" : "Create Failed";
        Console.WriteLine(message);
    }
    #endregion

    #region Update
    public void Update()
    {
        AppDbContext appDbContext = new AppDbContext();
        var category = appDbContext.TblProductCategories
                                   .FirstOrDefault(x => x.ProductCategoryId == 2 && x.DeleteFlag == false);

        if (category is null)
        {
            Console.WriteLine("No category found.");
            return;
        }

        category.ProductCategoryName = "Updated Category Name";
        int result = appDbContext.SaveChanges();

        var message = result > 0 ? "Update Successful" : "Update Failed";
        Console.WriteLine(message);
    }
    #endregion

    #region Delete
    public void Delete()
    {
        AppDbContext appDbContext = new AppDbContext();
        var category = appDbContext.TblProductCategories
                                   .FirstOrDefault(x => x.ProductCategoryId == 1 && x.DeleteFlag == false);

        if (category is null)
        {
            Console.WriteLine("No category found.");
            return;
        }

        category.DeleteFlag = true;
        int result = appDbContext.SaveChanges();

        var message = result > 0 ? "Delete Successful" : "Delete Failed";
        Console.WriteLine(message);
    }
    #endregion
}
