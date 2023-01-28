using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using DapperDemo.Helpers;
using DapperDemo.Models;

namespace DapperDemo
{
    public class Dal
    {
        private readonly SqlConnection _connection;

        public Dal(SqlConnection connection)
        {
            _connection = connection;
        }

        public void AddProduct(Product product)
        {
            var sql = "INSERT INTO products (name, description, weight, height, width, length) values" +
                              "(@Name, @Description, @Weight, @Height, @Width, @Length)";

            _connection.Execute(sql, product);
        }

        public Product GetProductById(int id)
        {
            var sql = "SELECT * FROM products WHERE id = @id";

            return _connection.Query<Product>(sql, new { id }).FirstOrDefault();
        }

        public void UpdateProduct(Product product)
        {
            var sql = "UPDATE products SET " +
                      "name = @Name," +
                      "description = @Description," +
                      "weight = @Weight," +
                      "height = @Height," +
                      "width = @Width," +
                      "length = @Length WHERE id = @Id";

            _connection.Execute(sql, product);
        }

        public void DeleteProduct(int id)
        {
            var sql = "DELETE FROM products WHERE id = @id";

            _connection.Execute(sql, new { id });
        }

        public List<Product> GetAllProducts()
        {
            var sql = "SELECT * FROM products";

            return _connection.Query<Product>(sql).ToList();
        }

        public void AddOrder(Order order)
        {
            var sql = "INSERT INTO orders (status, created_date, updated_date, product_id) values " +
                      "(@Status, @CreatedDate, @UpdatedDate, @ProductId)";

            _connection.Execute(sql, order);
        }

        public Order GetOrderById(int id)
        {
            var sql = "SELECT * FROM orders WHERE id = @id";

            return _connection.Query<Order>(sql, new { id }).FirstOrDefault();
        }

        public void UpdateOrder(Order order)
        {
            var sql = "UPDATE orders SET " +
                      "status = @Status," +
                      "created_date = @CreatedDate," +
                      "updated_date = @UpdatedDate," +
                      "product_id = @ProductId WHERE id = @Id";

            _connection.Execute(sql, order);
        }

        public void DeleteOrder(int id)
        {
            var sql = "DELETE FROM orders WHERE id = @id";

            _connection.Execute(sql, new { id });
        }

        public List<Order> GetAllOrders()
        {
            var sql = "SELECT * FROM orders";

            return _connection.Query<Order>(sql).ToList();
        }

        public List<Order> GetFilteredOrders(
            int? year = null,
            int? month = null,
            OrderStatus? status = null,
            int? product = null)
        {
            var orders = new List<Order>();

            using var cmd = new SqlCommand();
            cmd.Connection = _connection;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spGetFilteredOrders";

            cmd.Parameters.Add("@Year", SqlDbType.Int).Value = year;
            cmd.Parameters.Add("@Month", SqlDbType.Int).Value = month;
            cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = status;
            cmd.Parameters.Add("@Product", SqlDbType.Int).Value = product;

            var reader = cmd.ExecuteReader();

            while (reader.Read())
                orders.Add(new Order
                {
                    Id = reader["id"].ToInt(),
                    Status = reader["status"].ToOrderStatus(),
                    CreatedDate = reader["created_date"].ToDate(),
                    UpdatedDate = reader["updated_date"].ToDate(),
                    ProductId = reader["product_id"].ToInt()
                });

            reader.Close();
            return orders;
        }

        public void DeleteOrders(
            int? year = null,
            int? month = null,
            OrderStatus? status = null,
            int? product = null)
        {
            using var cmd = new SqlCommand();
            cmd.Connection = _connection;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spDeleteOrders";

            cmd.Parameters.Add("@Year", SqlDbType.Int).Value = year;
            cmd.Parameters.Add("@Month", SqlDbType.Int).Value = month;
            cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = status;
            cmd.Parameters.Add("@Product", SqlDbType.Int).Value = product;
            
            cmd.ExecuteNonQuery();
        }

        public void ClearAllData()
        {
            using var cmd = new SqlCommand();
            cmd.Connection = _connection;

            cmd.CommandText = "EXEC spClearDb;";
            cmd.ExecuteNonQuery();
        }
    }
}