using eP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace eP.Models
{
    public class ProductsToCategoryModel
    {
        [Key]
        //[Column(Order = 1)]
        public int ID { get; set; }
        public Product Product { get; set; }
        public Category Category { get; set; }
        public Supplier Supplier { get; set; }
    }
}