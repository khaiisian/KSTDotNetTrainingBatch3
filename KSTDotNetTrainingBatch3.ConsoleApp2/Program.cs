// See https://aka.ms/new-console-template for more information
using Microsoft.Data.SqlClient;
using System.Data;

Console.WriteLine("Hello, World!");

SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
sqlConnectionStringBuilder.DataSource = ".";
sqlConnectionStringBuilder.UserID = "sa";
sqlConnectionStringBuilder.Password = "sa@123";
sqlConnectionStringBuilder.TrustServerCertificate = true;
sqlConnectionStringBuilder.InitialCatalog = "MiniPOS";

SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);

connection.Open();
string query = @"SELECT [Id]
      ,[Name]
      ,[Email]
      ,[DateOfBirth]
      ,[Salary]
  FROM [dbo].[Tbl_Employee]";

SqlCommand cmd = new SqlCommand(query, connection);
SqlDataAdapter adapter = new SqlDataAdapter(cmd);
DataTable dt = new DataTable();
adapter.Fill(dt);
connection.Close();

for (int i = 0; i < dt.Rows.Count; i++)
{
    var row = dt.Rows[i];
    var salary = Convert.ToDecimal(row["Salary"]);
    Console.WriteLine("Id => " + row["Id"]);
    Console.WriteLine("Name => " + row["Name"]);
    Console.WriteLine("Date Of Birth => " + row["DateOfBirth"]);
    Console.WriteLine("Salary => "+ salary.ToString("n0") +"Kyats");
}