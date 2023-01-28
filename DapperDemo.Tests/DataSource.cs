using System;
using System.Collections.Generic;
using DapperDemo.Models;

namespace DapperDemo.Tests
{
    internal static class DataSource
    {
        public static readonly List<Product> Products = new List<Product>
        {
            new Product
            {
                Name = "OnePlus11",
                Description = "Smartphone",
                Weight = 215,
                Height = 10,
                Width = 75,
                Length = 176
            },
            new Product
            {
                Name = "Lenovo",
                Description = "Laptop",
                Weight = 3,
                Height = 25,
                Width = 65,
                Length = 55
            }
        };

        public static readonly List<Order> Orders = new List<Order>
        {
            new Order
            {
                Status = OrderStatus.NotStarted,
                CreatedDate = new DateTime(2022, 1, 7),
                UpdatedDate = new DateTime(2022, 12, 7),
                ProductId = 1
            },
            new Order
            {
                Status = OrderStatus.Done,
                CreatedDate = new DateTime(2023, 1, 17),
                UpdatedDate = new DateTime(2023, 2, 25),
                ProductId = 2
            },
            new Order
            {
                Status = OrderStatus.Arrived,
                CreatedDate = new DateTime(2025, 5, 3),
                UpdatedDate = new DateTime(2025, 8, 7),
                ProductId = 1
            },
            new Order
            {
                Status = OrderStatus.Loading,
                CreatedDate = new DateTime(2024, 1, 17),
                UpdatedDate = new DateTime(2024, 6, 25),
                ProductId = 2
            }
        };
    }
}