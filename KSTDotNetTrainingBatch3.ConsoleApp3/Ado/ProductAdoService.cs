using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSTDotNetTrainingBatch3.ConsoleApp3.Ado;

public class ProductAdoService
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
        string query = @"SELECT [ProductId]
                      ,[ProductName]
                      ,[Quantity]
                      ,[Price]
                      ,[DeleteFlag]
                       FROM [dbo].[Tbl_Product]
                       WHERE [DeleteFlag] = 0";

        SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
        connection.Open();
        SqlCommand cmd = new SqlCommand(query, connection);
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adapter.Fill(dt);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            var dr = dt.Rows[i];
            var price = Convert.ToDecimal(dr["Price"]);
            Console.WriteLine("ProductId => " + dr["ProductId"].ToString());
            Console.WriteLine("Product Name => " + dr["ProductName"].ToString());
            Console.WriteLine("Quantity => " + dr["Quantity"].ToString());
            Console.WriteLine("Price => " + Convert.ToDecimal(dr["Price"]).ToString("n2"));
            Console.WriteLine("Price0 => " + price.ToString("n"));
        }
    }
    #endregion

    #region Create
    public void Create()
    {
        string query = @"INSERT INTO [dbo].[Tbl_Product]
           ([ProductName]
           ,[Quantity]
           ,[Price]
           ,[DeleteFlag])
     VALUES
           ('Energy Drink'
           ,20
           ,2500
           ,0)";

        SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
        connection.Open();
        SqlCommand cmd = new SqlCommand(query, connection);
        var result = cmd.ExecuteNonQuery();

        string message = result > 0 ? "Create Successful!" : "Create Failed!";
        Console.WriteLine(message);
    }
    #endregion

    #region Update
    public void Update()
    {
        string query = @"UPDATE [dbo].[Tbl_Product]
                       SET [ProductName] = 'Basketball'
                          ,[Price] = 3500
                        WHERE ProductId = 12";
        SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
        connection.Open();
        SqlCommand cmd = new SqlCommand(query, connection);
        var result = cmd.ExecuteNonQuery();
        var message = result > 0 ? "Update Successful!" : "Update Failed";
        Console.WriteLine(message);
    }
    #endregion

    #region Delete
    public void Delete()
    {
        string query = @"DELETE FROM [dbo].[Tbl_Product]
      WHERE ProductId = 11";
        SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
        connection.Open();
        SqlCommand cmd = new SqlCommand(query, connection);
        var result = cmd.ExecuteNonQuery();
        var message = result > 0 ? "Delete Successful!" : "Delete Failed!";
        Console.WriteLine(message);
    }
    #endregion
}
