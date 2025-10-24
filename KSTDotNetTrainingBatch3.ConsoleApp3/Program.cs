// See https://aka.ms/new-console-template for more information
using KSTDotNetTrainingBatch3.ConsoleApp3.Ado;
using KSTDotNetTrainingBatch3.ConsoleApp3.Dapper;
using KSTDotNetTrainingBatch3.ConsoleApp3.EFCore;

Console.WriteLine("Hello, World!");

#region Ado.NetService
//ProductAdoService productService = new ProductAdoService();
//productService.Create();
//productService.Update();
//productService.Delete();
//productService.Read();

//SaleAdoService saleService = new SaleAdoService();
//saleService.Create();
//saleService.Read();
#endregion

#region DapperService
//ProductDapperService productDapperService = new ProductDapperService();
//productDapperService.Update();
//productDapperService.Create();
//productDapperService.Delete();
//productDapperService.Read();

//SaleDapperService saleDapperService = new SaleDapperService();
//saleDapperService.Read();
//saleDapperService.Create();
#endregion

#region EFCoreService
//ProductEFCoreService productEFCore = new ProductEFCoreService();
//productEFCore.Delete();
//productEFCore.Read();

SaleEFCoreService saleEFCoreService = new SaleEFCoreService();
saleEFCoreService.Create();
saleEFCoreService.Read();
#endregion