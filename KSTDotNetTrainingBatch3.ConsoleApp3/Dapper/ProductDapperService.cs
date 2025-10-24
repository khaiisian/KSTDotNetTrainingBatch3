using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSTDotNetTrainingBatch3.ConsoleApp3.Dapper;

public class ProductDapperService
{
    SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
    {
        DataSource = ".",
        InitialCatalog = "MiniPOS",
        UserID = "sa",
        Password = "sasa@123",
        TrustServerCertificate = true,
    };

    #region Read
    public void Read()
    {
        using (IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString))
        {
            string query = @"SELECT [ProductId]
                      ,[ProductName]
                      ,[Quantity]
                      ,[Price]
                      ,[DeleteFlag]
                       FROM [dbo].[Tbl_Product]
                       WHERE DeleteFlag = 0";

            db.Open();

            List<ProductDTO> prodcutList = db.Query<ProductDTO>(query).ToList();
            for (int i = 0; i < prodcutList.Count; i++)
            {
                var item = prodcutList[i];
                Console.WriteLine("Product Id => " + item.ProductID);
                Console.WriteLine("Product Name => " + item.ProductName);
                Console.WriteLine("Product Quantity => " + item.Quantity);
                Console.WriteLine("Product Price => " + item.Price.ToString("n"));
            }
        };
    }
    #endregion

    #region Create
    public void Create()
    {
        using (IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString))
        {
            db.Open();
            string query = @"INSERT INTO [dbo].[Tbl_Product]
                                   ([ProductName]
                                   ,[Quantity]
                                   ,[Price]
                                   ,[DeleteFlag])
                             VALUES
                                   ('Hot Wheel'
                                   ,20
                                   ,2500
                                   ,0)";

            var result = db.Execute(query);
            string message = result > 0 ? "Create Successful!" : "Create Failed!";
            Console.WriteLine(message);
        }
    }
    #endregion

    #region Update
    public void Update()
    {
        using (IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString))
        {
            db.Open();

            string query = @"UPDATE [dbo].[Tbl_Product]
                          SET [Quantity] = 50
                          ,[Price] = 3500
                        WHERE ProductId = 7 AND DeleteFlag = 0";

            int result = db.Execute(query);
            var message = result > 0 ? "Update Successful!" : "Update Failed";
            Console.WriteLine(message);
        }
    }
    #endregion

    #region Delete
    public void Delete()
    {
        using (IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString))
        {
            db.Open();

            string query = @"UPDATE Tbl_Product SET DeleteFlag = 1 Where ProductId = 14";
            var result = db.Execute(query);

            var message = result > 0 ? "Delete Successful!" : "Delete Failed!";
            Console.WriteLine(message);
        }
    }
    #endregion
}

public class ProductDTO
{
    public int ProductID { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
