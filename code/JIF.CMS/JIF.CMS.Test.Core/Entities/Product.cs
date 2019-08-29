using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.CMS.Test.Core.Entities
{
    public class Product
    {
        public DateTime CreateTime { get; set; }
        public int SysNo { get; set; }
        public string ProductId { get; set; }
        public decimal Price { get; set; }
        public ProductiontAddress productiontAddress { get; set; }
        public List<string> Tags { get; set; }
        public List<ProductAttribute> attributes { get; set; }
        public Product subProduct { get; set; }
    }

    public class ProductAttribute
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class ProductiontAddress
    {
        public string Address { get; set; }
    }
}
