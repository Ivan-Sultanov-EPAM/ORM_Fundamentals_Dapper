using System;
using System.Data.SqlClient;

namespace DapperDemo.Tests
{
    public class TestBase : IDisposable
    {
        private readonly string _connectionString =
            "Data Source=(localdb)\\MSSQLLocalDB;Database = DapperDemoDb;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=5;Encrypt=False;TrustServerCertificate=False";

        public readonly SqlConnection Connection;
        public readonly Dal Dal;

        public TestBase()
        {
            Connection = new SqlConnection(_connectionString);
            Connection.Open();

            Dal = new Dal(Connection);
        }

        public void Dispose()
        {
            Dal.ClearAllData();
            Connection.Dispose();
        }
    }
}