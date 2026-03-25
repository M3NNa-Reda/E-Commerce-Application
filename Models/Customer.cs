using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce_Application.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime CreatedAt { get; set; }
        public virtual CustomerProfile CustomerProfile { get; set; }
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
        public override string ToString()
        {
            return $"CustomerId:{CustomerId} , FullName:{FullName} , Email:{Email} , PhoneNumber:{PhoneNumber}";
        }


    }
}
