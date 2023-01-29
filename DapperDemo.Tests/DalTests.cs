using System.Collections.Generic;
using DapperDemo.Models;
using FluentAssertions;
using Xunit;

namespace DapperDemo.Tests
{
    public class DalTests : TestBase
    {
        [Fact]
        public void Should_Add_Product()
        {
            var product = DataSource.Products[0];

            Dal.AddProduct(product);

            Dal.GetAllProducts().Should()
                .BeEquivalentTo(new List<Product> { product },
                    config => config
                        .Excluding(p => p.Id));
        }

        [Fact]
        public void Should_Get_All_Products()
        {
            var product1 = DataSource.Products[0];
            var product2 = DataSource.Products[1];

            Dal.AddProduct(product1);
            Dal.AddProduct(product2);

            Dal.GetAllProducts().Should()
                .BeEquivalentTo(new List<Product> { product1, product2 },
                    config => config
                        .Excluding(p => p.Id));
        }

        [Fact]
        public void Should_Update_Product()
        {
            var product1 = DataSource.Products[0];
            var product2 = DataSource.Products[1];
            product2.Id = 1;

            Dal.AddProduct(product1);
            Dal.UpdateProduct(product2);

            Dal.GetAllProducts().Should()
                .BeEquivalentTo(new List<Product> { product2 });
        }

        [Fact]
        public void Should_Get_Product_By_Id()
        {
            var product1 = DataSource.Products[0];
            var product2 = DataSource.Products[1];

            Dal.AddProduct(product1);
            Dal.AddProduct(product2);

            Dal.GetProductById(1).Should()
                .BeEquivalentTo(product1, config =>
                    config.Excluding(p => p.Id));

            Dal.GetProductById(2).Should()
                .BeEquivalentTo(product2, config =>
                    config.Excluding(p => p.Id));
        }

        [Fact]
        public void Should_Delete_Product()
        {
            var product1 = DataSource.Products[0];
            var product2 = DataSource.Products[1];

            Dal.AddProduct(product1);
            Dal.AddProduct(product2);

            Dal.DeleteProduct(1);

            Dal.GetAllProducts().Should()
                .BeEquivalentTo(new List<Product> { product2 },
                    config => config
                        .Excluding(p => p.Id));
        }

        [Fact]
        public void Should_Add_Order()
        {
            AddProducts();

            var order = DataSource.Orders[0];

            Dal.AddOrder(order);
            
            Dal.GetAllOrders().Should()
                .BeEquivalentTo(new List<Order> { order },
                    config => config
                        .Excluding(p => p.Id));
        }

        [Fact]
        public void Should_Get_All_Orders()
        {
            AddProducts();

            var order1 = DataSource.Orders[0];
            var order2 = DataSource.Orders[1];

            Dal.AddOrder(order1);
            Dal.AddOrder(order2);

            Dal.GetAllOrders().Should()
                .BeEquivalentTo(new List<Order> { order1, order2 },
                    config => config
                        .Excluding(p => p.Id));
        }

        [Fact]
        public void Should_Update_Order()
        {
            AddProducts();

            var order1 = DataSource.Orders[0];
            var order2 = DataSource.Orders[1];
            Dal.AddOrder(order1);
            Dal.AddOrder(order2);

            order1.Id = 1;
            order1.Status = OrderStatus.InProgress;
            order1.UpdatedDate = order1.UpdatedDate.AddDays(10);

            Dal.UpdateOrder(order1);

            Dal.GetAllOrders().Should()
                .BeEquivalentTo(new List<Order> { order1, order2 },
                    config => config
                        .Excluding(o => o.Id));
        }

        [Fact]
        public void Should_Get_Order_By_Id()
        {
            AddProducts();

            var order1 = DataSource.Orders[0];
            var order2 = DataSource.Orders[1];
            Dal.AddOrder(order1);
            Dal.AddOrder(order2);

            Dal.GetOrderById(1).Should()
                .BeEquivalentTo(order1, config =>
                    config.Excluding(p => p.Id));

            Dal.GetOrderById(2).Should()
                .BeEquivalentTo(order2, config =>
                    config.Excluding(p => p.Id));
        }

        [Fact]
        public void Should_Delete_Order()
        {
            AddProducts();

            var order1 = DataSource.Orders[0];
            var order2 = DataSource.Orders[1];
            Dal.AddOrder(order1);
            Dal.AddOrder(order2);

            Dal.DeleteOrder(1);

            Dal.GetAllOrders().Should()
                .BeEquivalentTo(new List<Order> { order2 },
                    config => config
                        .Excluding(p => p.Id));
        }

        [Theory]
        [MemberData(nameof(GetFilteredOrdersTestData))]
        public void Should_Get_Filtered_Orders(
            int? year,
            int? month,
            OrderStatus? status,
            int? product,
            List<Order> expected)
        {
            AddProducts();

            Dal.AddOrder(DataSource.Orders[0]);
            Dal.AddOrder(DataSource.Orders[1]);
            Dal.AddOrder(DataSource.Orders[2]);
            Dal.AddOrder(DataSource.Orders[3]);

            var result = Dal.GetFilteredOrders(
                year: year,
                month: month,
                status: status,
                product: product
                );

            result.Should()
                .BeEquivalentTo(expected, config => config
                         .Excluding(p => p.Id));
        }

        [Theory]
        [MemberData(nameof(DeleteOrdersTestData))]
        public void Should_Delete_Orders(
            int? year,
            int? month,
            OrderStatus? status,
            int? product,
            List<Order> expected)
        {
            AddProducts();

            Dal.AddOrder(DataSource.Orders[0]);
            Dal.AddOrder(DataSource.Orders[1]);
            Dal.AddOrder(DataSource.Orders[2]);
            Dal.AddOrder(DataSource.Orders[3]);

            Dal.DeleteOrders(
                year: year,
                month: month,
                status: status,
                product: product
            );

            var result = Dal.GetAllOrders();

            result.Should()
                .BeEquivalentTo(expected, config => config
                    .Excluding(p => p.Id));
        }

        private void AddProducts()
        {
            Dal.AddProduct(DataSource.Products[0]);
            Dal.AddProduct(DataSource.Products[1]);
        }

        public static IEnumerable<object[]> GetFilteredOrdersTestData()
        {
            var data = new List<object[]>
            {
                new object[] { null, null, null, null, new List<Order>
                {
                    DataSource.Orders[0],
                    DataSource.Orders[1],
                    DataSource.Orders[2],
                    DataSource.Orders[3]
                }},
                new object[] { 2023, null, null, null, new List<Order>
                {
                    DataSource.Orders[1]
                }},
                new object[] { null, 5, null, null, new List<Order>
                {
                    DataSource.Orders[2]
                }},
                new object[] { null, null, OrderStatus.Loading, null, new List<Order>
                {
                    DataSource.Orders[3]
                }},
                new object[] { null, null, null, 2, new List<Order>
                {
                    DataSource.Orders[1],
                    DataSource.Orders[3]
                }},
                new object[] { 2024, 1, null, null, new List<Order>
                {
                    DataSource.Orders[3]
                }},
                new object[] { null, 1, OrderStatus.Loading, null, new List<Order>
                {
                    DataSource.Orders[3]
                }},
                new object[] { 2023, 1, OrderStatus.Done, 2, new List<Order>
                {
                    DataSource.Orders[1]
                }}
            };

            return data;
        }

        public static IEnumerable<object[]> DeleteOrdersTestData()
        {
            var data = new List<object[]>
            {
                new object[] { null, null, null, null, new List<Order>()},
                new object[] { 2023, null, null, null, new List<Order>
                {
                    DataSource.Orders[0],
                    DataSource.Orders[2],
                    DataSource.Orders[3]
                }},
                new object[] { null, 5, null, null, new List<Order>
                {
                    DataSource.Orders[0],
                    DataSource.Orders[1],
                    DataSource.Orders[3]
                }},
                new object[] { null, null, OrderStatus.Loading, null, new List<Order>
                {
                    DataSource.Orders[0],
                    DataSource.Orders[1],
                    DataSource.Orders[2]
                }},
                new object[] { null, null, null, 2, new List<Order>
                {
                    DataSource.Orders[0],
                    DataSource.Orders[2]
                }},
                new object[] { 2024, 1, null, null, new List<Order>
                {
                    DataSource.Orders[0],
                    DataSource.Orders[1],
                    DataSource.Orders[2]
                }},
                new object[] { null, 1, OrderStatus.Loading, null, new List<Order>
                {
                    DataSource.Orders[0],
                    DataSource.Orders[1],
                    DataSource.Orders[2]
                }},
                new object[] { 2023, 1, OrderStatus.Done, 2, new List<Order>
                {
                    DataSource.Orders[0],
                    DataSource.Orders[2],
                    DataSource.Orders[3]
                }}
            };

            return data;
        }
    }
}