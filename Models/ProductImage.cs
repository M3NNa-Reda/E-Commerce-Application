using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce_Application.Models
{
    public class ProductImage
    {
        public int ProductImageId { get; set; }
        public string Url { get; set; }
        public string AltText { get; set; }
        public bool IsPrimary { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }


    }
}
