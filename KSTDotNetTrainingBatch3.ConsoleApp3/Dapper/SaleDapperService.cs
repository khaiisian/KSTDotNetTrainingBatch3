using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSTDotNetTrainingBatch3.ConsoleApp3.Dapper;

public class SaleDapperService
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
        string query = @"SELECT [SaleId]
                              ,[ProductId]
                              ,[Quantity]
                              ,[Price]
                              ,[DeleteFlag]
                              ,[CreatedDateTime]
                              ,[ModifiedDateTime]
                          FROM [dbo].[Tbl_Sale]
                          WHERE DeleteFlag = 0";

        using (IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString))
        {
            db.Open();
            
            List<SaleDto> lst = db.Query<SaleDto>(query).ToList();
            for (int i = 0; i < lst.Count; i++)
            {
                var item = lst[i];
                Console.WriteLine("SaleId =>" + item.SaleId);
                Console.WriteLine("ProductId =>" + item.ProductId);
                Console.WriteLine("Quantity =>" + item.Quantity);
                Console.WriteLine("Price =>" + item.Price);
            }
        }
    }
    #endregion

    #region Create
    public void Create()
    {
        using (IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString))
        {            
            db.Open();

            var query1 = "select * from Tbl_Product where ProductId = 9 and DeleteFlag = 0";
            var item = db.Query<ProductDTO>(query1).FirstOrDefault();
            if(item is null)
            {
                Console.WriteLine("No item found");
                return;
            }

            var existingQty = item.Quantity;
            if(existingQty < 20)
            {
                Console.WriteLine("There is not enough quantity left for item.");
                return;
            }

            string query2 = @"INSERT INTO [dbo].[Tbl_Sale]
                               ([ProductId]
                               ,[Quantity]
                               ,[Price]
                               ,[DeleteFlag]
                               ,[CreatedDateTime]
                               ,[ModifiedDateTime])
                                VALUES
                               (9, 20, 3800.00, 0, GETDATE(), NULL)";
            int result2 = db.Execute(query2);
            if(result2 <= 0)
            {
                Console.WriteLine("Sale creation failed");
                return;
            }

            var updatedQty = existingQty - 20;

            string query3 = @"UPDATE Tbl_Product
                              SET Quantity = @Quantity
                              ,ModifiedDateTime = GETDATE()
                              WHERE ProductId = 9";
            var result = db.Execute(query3, new SaleDto { Quantity = updatedQty});
            var message = result > 0 ? "Sale creation successful." : "Sale creation failed.";
            Console.WriteLine(message);
        }
        ;
    }
    #endregion
}
public class SaleDto
{
    public int SaleId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public bool DeleteFlag { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public DateTime? ModifiedDateTime { get; set; }
}

