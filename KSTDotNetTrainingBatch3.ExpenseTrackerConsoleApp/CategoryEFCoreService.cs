using KSTDotNetTrainingBatch3.ExpenseTrackerDatabase.AppDbContextModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSTDotNetTrainingBatch3.ExpenseTrackerConsoleApp;

public class CategoryEFCoreService
{
    #region "Category Menu"
    public void Menu()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("1. View Categories.");
            Console.WriteLine("2. Add Category.");
            Console.WriteLine("3. Update Category.");
            Console.WriteLine("4. Delete Category.");
            Console.WriteLine("5. Exit.");

            var menuNo = Console.ReadLine();

            switch (menuNo)
            {
                case "1":
                    Read();
                    break;
                case "2":
                    Create();
                    break;
                case "3":
                    Update();
                    break;
                case "4":
                    Delete();
                    break;
                case "5":
                    Console.WriteLine("You have exited the program.");
                    return;                
                default:
                    Console.WriteLine("Invalid Option.");
                    break;
            }
        }
    }
    #endregion

    #region Category List
    public void Read()
    {
        AppDbContext appDbContext = new AppDbContext();
        var lst = appDbContext.TblCategories
            .Where(x=>x.DeleteFlag == false)
            .ToList();
        Console.WriteLine($"{"ID",4} | {"Name",-20}");
        Console.WriteLine($"------------------------");

        foreach (var item in lst)
        {
            Console.WriteLine($"{item.CategoryId,4} | {item.CategoryName,-20}");
        }

    }
    #endregion

    #region Add Category
    public void Create()
    {
        AppDbContext appDbContext = new AppDbContext();
        while (true)
        {
            Console.WriteLine();
            Console.Write("Enter Category Name: ");
            string? categoryName = Console.ReadLine();

            if (string.IsNullOrEmpty(categoryName))
            {
                Console.WriteLine("Category name cannot be empty.");
                continue;
            }

            var category = new TblCategory
            {
                CategoryName = categoryName,
                CreatedDateTime = DateTime.Now,
                DeleteFlag = false,
            };

            appDbContext.TblCategories.Add(category);
            int result = appDbContext.SaveChanges();

            var message = result > 0 ? "Category is added successfully." : "Category is not added successfully.";
            Console.WriteLine(message);
            break;
        }        
    }
    #endregion

    #region Update Category
    public void Update()
    {
        AppDbContext appDbContext = new AppDbContext();
        while (true)
        {
            Console.WriteLine();
            Console.Write("Enter Category ID: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid Id!");
                continue;
            }

            var category = appDbContext.TblCategories
                .Where(x => x.DeleteFlag == false)
                .FirstOrDefault(x => x.CategoryId == id);

            if (category == null)
            {
                Console.WriteLine("No category found.");
                continue;
            }

            Console.WriteLine();
            Console.WriteLine("Category Details");
            Console.WriteLine($"{"Category ID",-20}: {category.CategoryId}");
            Console.WriteLine($"{"Category Name",-20}: {category.CategoryName}");

            Console.WriteLine();
            Console.Write("Enter New Category Name: ");
            string? categoryName = Console.ReadLine();

            if (string.IsNullOrEmpty(categoryName))
            {
                Console.WriteLine("Category name cannot be empty.");
                continue;
            }

            category.CategoryName = categoryName;
            category.ModifiedDateTime = DateTime.Now;
            int result = appDbContext.SaveChanges();

            var message = result > 0 ? "Category is udpated successfully." : "Category is not updated successfully.";
            Console.WriteLine(message);
            break;
        }
    }
    #endregion

    #region Delete Category
    public void Delete()
    {
        while (true)
        {
            Console.WriteLine();
            AppDbContext appDbContext = new AppDbContext();
            Console.Write("Category ID: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid Id!");
                continue;
            }

            var category = appDbContext.TblCategories
                .Where(x => x.DeleteFlag == false)
                .FirstOrDefault(x => x.CategoryId == id);

            if (category == null)
            {
                Console.WriteLine("No category found.");
                continue;
            }

            category.DeleteFlag = true;
            category.ModifiedDateTime = DateTime.Now;
            int result = appDbContext.SaveChanges();

            var message = result > 0 ? "Category is deleted successfully." : "Category is not deleted successfully.";
            Console.WriteLine(message); 
            break;
        }
    }
    #endregion
}
