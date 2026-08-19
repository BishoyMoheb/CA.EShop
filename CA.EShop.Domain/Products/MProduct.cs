using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.EShop.Domain.Products
{
    public class MProduct
    {
        public long ProdID { get; set; }
        public string ProdName { get; set; } = string.Empty;
        public decimal ProdPrice { get; set; }
        public List<string> LStr_Tags { get; set; } = new();
    }
}
