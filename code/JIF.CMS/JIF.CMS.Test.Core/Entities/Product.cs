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
    }
}
