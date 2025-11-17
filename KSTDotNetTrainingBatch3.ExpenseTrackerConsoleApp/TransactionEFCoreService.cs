using KSTDotNetTrainingBatch3.ExpenseTrackerDatabase.AppDbContextModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSTDotNetTrainingBatch3.ExpenseTrackerConsoleApp;

public class TransactionEFCoreService
{
    #region Add Transaction
    public void Create(string type)
    {
        AppDbContext appDbContext = new AppDbContext();

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine($"Add {type} ---------");
            var categories = appDbContext.TblCategories
                        .Where(x => x.DeleteFlag == false)
                        .ToList();

            Console.WriteLine("Select Category:");
            foreach (var cat in categories)
            {
                Console.WriteLine($"{cat.CategoryId}. {cat.CategoryName}");
            }
            Console.Write("Enter Category ID: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid Id!");
                continue;
            }
        }

    }
    #endregion
}
