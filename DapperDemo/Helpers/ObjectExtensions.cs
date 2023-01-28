using System;
using DapperDemo.Models;

namespace DapperDemo.Helpers
{
    public static class ObjectExtensions
    {
        public static OrderStatus ToOrderStatus(this object status)
        {
            return status.ToString() switch
            {
                "0" => OrderStatus.NotStarted,
                "1" => OrderStatus.Loading,
                "2" => OrderStatus.InProgress,
                "3" => OrderStatus.Arrived,
                "4" => OrderStatus.Unloading,
                "5" => OrderStatus.Cancelled,
                "6" => OrderStatus.Done,
                _ => throw new ArgumentOutOfRangeException(nameof(status))
            };
        }

        public static decimal ToDecimal(this object value)
        {
            if (decimal.TryParse(value.ToString(), out var result)) return result;

            throw new ArgumentException(nameof(value));
        }

        public static int ToInt(this object value)
        {
            if (int.TryParse(value.ToString(), out var result)) return result;

            throw new ArgumentException(nameof(value));
        }

        public static DateTime ToDate(this object value)
        {
            if (DateTime.TryParse(value.ToString(), out var result)) return result;

            throw new ArgumentException(nameof(value));
        }
    }
}