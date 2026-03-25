using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce_Application.Models
{
    public class CustomerProfile
    {
        
        public int CustomerProfileId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string NationalId { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public override string ToString()
        {
            return $"CustomerProfileId:{CustomerProfileId} , Address:{Address} ";
        }

    }
}
