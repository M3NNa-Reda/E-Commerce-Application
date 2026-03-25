using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce_Application.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime PlacedAt { get; set; }
        public DateTime? ShippedAt { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual Payment Payment { get; set; }
    }
    public enum OrderStatus
    {
        Pending,
        Shipped,
        Delivered,
        Cancelled,
        Refunded
    }
}
