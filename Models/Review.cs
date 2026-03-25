using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce_Application.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual Product Product { get; set; }
        public virtual Customer Customer { get; set; }
        public override string ToString()
        {
            return $"ReviewId: {ReviewId} , CustomerId: {CustomerId} , Rating: {Rating} ";
        }
    }
}
