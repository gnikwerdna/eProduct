using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eProduct.Models
{
    public class ProdCatSup
    {
        public Product productData { get; set; }
        public Category categoryData { get; set; }
        public Supplier supplierData { get; set; }
        public File fileData { get; set; }
    }
}