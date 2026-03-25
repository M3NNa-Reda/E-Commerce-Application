using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce_Application.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int OrderId { get; set; }
        public string Method { get; set; }
        public PaymentStatus Status { get; set; }
        public DateTime? PaidAt { get; set; }
        public decimal Amount { get; set; }
        public virtual Order Order { get; set; }
    }
    public enum PaymentStatus
    {
        Pending,
        Paid,
        Refunded
    }
}
