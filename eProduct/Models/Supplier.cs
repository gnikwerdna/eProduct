using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eProduct.Models
{
    public class Supplier
    {
        [Key]
        public int SupplierID { get; set; }
        [Display(Name ="Company Name")]
        public string CompanyName { get; set; }
        [Display(Name = "Company Contact")]
        public string CompanyContact { get; set; }
    }
}