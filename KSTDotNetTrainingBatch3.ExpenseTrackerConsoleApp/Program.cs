// See https://aka.ms/new-console-template for more information
using KSTDotNetTrainingBatch3.ExpenseTrackerConsoleApp;
using System.ComponentModel;

Console.WriteLine("Hello, World!");
CategoryEFCoreService categoryEFCoreService = new CategoryEFCoreService();


while (true)
{
    Console.WriteLine();
    Console.WriteLine("1. Add Income.");
    Console.WriteLine("2. Add Expense.");
    Console.WriteLine("3. Manage Category.");
    Console.WriteLine("4. View All Transactions.");
    Console.WriteLine("6. Exit.");

    string? menuNo = Console.ReadLine();

    switch (menuNo)
    {
        case "1":
            Console.WriteLine("Option 1.");
            break;
        case "2":
            Console.WriteLine("Option 2.");
            break;
        case "3":
            Console.WriteLine("Option 3.");
            categoryEFCoreService.Menu();
            break;
        case "4":
            Console.WriteLine("Option 4.");
            break;
        case "5":
            Console.WriteLine("Option 5.");
            break;
        case "6":
            Console.WriteLine("You have exited the program.");
            return;
        default:
            Console.WriteLine("Invalid Option.");
            break;
    }
}