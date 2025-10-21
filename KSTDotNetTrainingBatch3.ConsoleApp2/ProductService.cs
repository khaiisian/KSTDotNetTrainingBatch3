using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSTDotNetTrainingBatch3.ConsoleApp2;

public class ProductService
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
                       FROM [dbo].[Tbl_Product]";

        SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
        connection.Open();
        SqlCommand cmd = new SqlCommand(query, connection);
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adapter.Fill(dt);

        foreach (DataRow dr in dt.Rows)
        {
            var price = Convert.ToDecimal(dr["Price"]);
            Console.WriteLine("ProductId => " + dr["ProductId"].ToString());
            Console.WriteLine("Product Name => " + dr["ProductName"].ToString());
            Console.WriteLine("Quantity => " + dr["Quantity"].ToString());
            Console.WriteLine("Price => " + Convert.ToDecimal(dr["Price"]).ToString("n2"));
            Console.WriteLine("Price0 => " +price.ToString("n"));
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
           ('Apple'
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
                       SET [ProductName] = 'Dragon Fruit'
                          ,[Quantity] = 5
                          ,[Price] = 3500
                          ,[DeleteFlag] = 0
                        WHERE ProductId = 3";
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
      WHERE ProductId = 4";
        SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
        connection.Open();
        SqlCommand cmd = new SqlCommand(query, connection);
        var result = cmd.ExecuteNonQuery();
        var message = result > 0 ? "Delete Successful!" : "Delete Failed!";
        Console.WriteLine(message);
    }
    #endregion
}
