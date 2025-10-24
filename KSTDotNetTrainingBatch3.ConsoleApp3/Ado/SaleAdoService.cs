using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSTDotNetTrainingBatch3.ConsoleApp3.Ado;

public class SaleAdoService
{
    SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
    {
        DataSource = ".",
        UserID = "sa",
        Password = "sasa@123",
        TrustServerCertificate = true,
        InitialCatalog = "MiniPOS"
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
                      FROM [dbo].[Tbl_Sale]";

        SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
        connection.Open();
        SqlCommand cmd = new SqlCommand(query, connection);
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adapter.Fill(dt);
        connection.Close();

        foreach(DataRow dr in dt.Rows)
        {
            Console.WriteLine("Sale Id =>" + dr["SaleId"]);
            Console.WriteLine("Product Id =>" + dr["ProductId"]);
            Console.WriteLine("Quantity =>" + dr["Quantity"]);
            Console.WriteLine("Price =>" + dr["Price"]);
        }
    }
    #endregion

    #region Create
    public void Create()
    {        
        SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
        connection.Open();

        var query1 = "select Quantity from Tbl_Product where ProductId = 9 and DeleteFlag = 0";
        SqlCommand cmd1 = new SqlCommand(query1, connection);
        SqlDataAdapter adapter = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        adapter.Fill(dt1);

        if(dt1.Rows.Count <= 0)
        {
            Console.WriteLine("No Item Found.");
            return;
        }

        var existingQty = Convert.ToInt32(dt1.Rows[0]["Quantity"]);
        if (existingQty < 15)
        {
            Console.WriteLine("There is not enough quantity left for that product.");
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
                       (9, 15, 3800.00, 0, GETDATE(), NULL)";
        SqlCommand cmd2 = new SqlCommand(query2, connection);
        int result1 = cmd2.ExecuteNonQuery();
        if(result1 <= 0)
        {
            Console.WriteLine("Sale creation failed.");
            return;
        }

        var updatedQty = existingQty - 15;
        string query3 = @"UPDATE Tbl_Product
                          SET Quantity = @updateQty
                         ,ModifiedDateTime = GETDATE()
                          WHERE ProductId = 9";
        SqlCommand cmd3 = new SqlCommand(query3, connection);
        cmd3.Parameters.AddWithValue("@updateQty", updatedQty);
        int result3 = cmd3.ExecuteNonQuery();

        var message = result3 > 0 ? "Sale creation successful." : "Sale creation failed.";
        Console.WriteLine(message);

        connection.Close();
    }
    #endregion
}
