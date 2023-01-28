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

            return _connection.QueryFirstOrDefault<Product>(sql, new { id });
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
            var sql = "INSERT INTO orders (status, createdDate, updatedDate, productId) values " +
                      "(@Status, @CreatedDate, @UpdatedDate, @ProductId)";

            _connection.Execute(sql, order);
        }

        public Order GetOrderById(int id)
        {
            var sql = "SELECT * FROM orders WHERE id = @id";

            return _connection.QueryFirstOrDefault<Order>(sql, new { id });
        }

        public void UpdateOrder(Order order)
        {
            var sql = "UPDATE orders SET " +
                      "status = @Status," +
                      "createdDate = @CreatedDate," +
                      "updatedDate = @UpdatedDate," +
                      "productId = @ProductId WHERE id = @Id";

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
            var sql = "spGetFilteredOrders";

            return _connection.Query<Order>(sql, new
            {
                Year = year,
                Month = month,
                Status = status,
                Product = product
            }, commandType: CommandType.StoredProcedure).ToList();
        }

        public void DeleteOrders(
            int? year = null,
            int? month = null,
            OrderStatus? status = null,
            int? product = null)
        {
            var sql = "spDeleteOrders";

            _connection.Execute(sql, new
            {
                Year = year,
                Month = month,
                Status = status,
                Product = product
            }, commandType: CommandType.StoredProcedure);
        }

        public void ClearAllData()
        {
            var sql = "spClearDb";

            _connection.Execute(sql, commandType: CommandType.StoredProcedure);
        }
    }
}