using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce_Application.Models
{
    public class Tag
    {
        public int TagId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ProductTag> productTags { get; set; }
        public override string ToString()
        {
            return $"TagId: {TagId} , Name: {Name} ";
        }
    }
}
